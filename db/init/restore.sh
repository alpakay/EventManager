#!/usr/bin/env bash
set -euo pipefail

# ---- Config ----
DB_NAME="${DB_NAME:-EventManagerDb}"
BAK_PATH="/var/opt/mssql/backup/${DB_NAME}.bak"

# Parola: önce SA_PASSWORD, yoksa MSSQL_SA_PASSWORD
PASS="${SA_PASSWORD:-${MSSQL_SA_PASSWORD:-}}"
if [ -z "${PASS}" ]; then
  echo "ERROR: SA password not provided (set SA_PASSWORD or MSSQL_SA_PASSWORD in .env)"
  exit 2
fi

# sqlcmd yolu (v18 veya eski)
SQLCMD="/opt/mssql-tools18/bin/sqlcmd"
[ -x "$SQLCMD" ] || SQLCMD="/opt/mssql-tools/bin/sqlcmd"
echo ">> Using sqlcmd at: $SQLCMD"
if [ ! -x "$SQLCMD" ]; then
  echo "ERROR: sqlcmd not found in image."
  exit 3
fi

echo ">> Restore script started"
echo "   DB_NAME=${DB_NAME}"
echo "   BAK_PATH=${BAK_PATH}"

# .bak dosyası var mı?
if [ ! -f "${BAK_PATH}" ]; then
  echo "ERROR: Backup file not found at ${BAK_PATH}"
  ls -l /var/opt/mssql/backup || true
  exit 4
fi

# MSSQL hazır mı?
echo ">> Waiting MSSQL..."
ready=0
for i in $(seq 1 60); do
  if "$SQLCMD" -S db -U sa -P "$PASS" -Q "SELECT 1" >/dev/null 2>&1; then
    ready=1; break
  fi
  sleep 2
  echo ">> Waiting MSSQL..."
done
if [ "$ready" -ne 1 ]; then
  echo "ERROR: MSSQL did not become ready in time."
  exit 5
fi

TEMP_SQL="/tmp/restore_$(date +%s).sql"
sed -e "s|@@DBNAME@@|$DB_NAME|g" \
    -e "s|@@BAKPATH@@|$BAK_PATH|g" \
    /restore.sql > "$TEMP_SQL"

echo ">> Running restore.sql ..."
"$SQLCMD" -S db -U sa -P "$PASS" -b -i "$TEMP_SQL"

echo ">> Restore completed."
rm -f "$TEMP_SQL"
