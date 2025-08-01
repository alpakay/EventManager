using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerApp.Migrations
{
    /// <inheritdoc />
    public partial class cryptionKeyInsertion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    KeyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KeyValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.KeyId);
                });

            migrationBuilder.InsertData(
                table: "Keys",
                columns: new[] { "KeyId", "KeyValue" },
                values: new object[] { 1, "z4sbUQxWv/fbiQv4OnQYzjTwcbNTe9I9KR2DZhBPUrQ=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keys");
        }
    }
}
