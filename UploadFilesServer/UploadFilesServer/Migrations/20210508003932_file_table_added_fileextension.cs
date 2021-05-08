using Microsoft.EntityFrameworkCore.Migrations;

namespace UploadFilesServer.Migrations
{
    public partial class file_table_added_fileextension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Files");
        }
    }
}
