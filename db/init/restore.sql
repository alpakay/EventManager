DECLARE @db  SYSNAME        = N'@@DBNAME@@';
DECLARE @bak NVARCHAR(4000) = N'@@BAKPATH@@';

IF DB_ID(@db) IS NOT NULL
BEGIN
    PRINT 'Dropping existing database ' + @db;
    EXEC('ALTER DATABASE [' + @db + '] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;');
    EXEC('DROP DATABASE [' + @db + ']');
END

DECLARE @dataLogical NVARCHAR(128);
DECLARE @logLogical  NVARCHAR(128);

-- Simplified table structure that matches modern SQL Server versions
DECLARE @files TABLE (
    LogicalName NVARCHAR(128),
    PhysicalName NVARCHAR(260),
    Type CHAR(1),
    FileGroupName NVARCHAR(128),
    Size NUMERIC(20,0),
    MaxSize NUMERIC(20,0),
    FileId INT,
    CreateLSN NUMERIC(25,0),
    DropLSN NUMERIC(25,0),
    UniqueId UNIQUEIDENTIFIER,
    ReadOnlyLSN NUMERIC(25,0),
    ReadWriteLSN NUMERIC(25,0),
    BackupSizeInBytes BIGINT,
    SourceBlockSize INT,
    FileGroupId INT,
    LogGroupGUID UNIQUEIDENTIFIER,
    DifferentialBaseLSN NUMERIC(25,0),
    DifferentialBaseGUID UNIQUEIDENTIFIER,
    IsReadOnly BIT,
    IsPresent BIT,
    TDEThumbprint VARBINARY(32),
    SnapshotUrl NVARCHAR(360)
);

INSERT INTO @files
EXEC('RESTORE FILELISTONLY FROM DISK = ''' + @bak + '''');

SELECT 
  @dataLogical = (SELECT TOP 1 LogicalName FROM @files WHERE Type='D' ORDER BY FileId),
  @logLogical  = (SELECT TOP 1 LogicalName FROM @files WHERE Type='L' ORDER BY FileId);

DECLARE @sql NVARCHAR(MAX) = N'
RESTORE DATABASE [' + @db + N']
FROM DISK = N''' + @bak + N'''
WITH MOVE ''' + @dataLogical + N''' TO ''/var/opt/mssql/data/' + @db + N'.mdf'',
     MOVE ''' + @logLogical  + N''' TO ''/var/opt/mssql/data/' + @db + N'_log.ldf'',
     REPLACE, STATS=5;';

PRINT @sql;
EXEC(@sql);