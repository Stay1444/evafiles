using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaFiles.Migrations
{
    /// <inheritdoc />
    public partial class DurationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DownloadCount",
                table: "files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExpireDate",
                table: "files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadCount",
                table: "files");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "files");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "files");
        }
    }
}
