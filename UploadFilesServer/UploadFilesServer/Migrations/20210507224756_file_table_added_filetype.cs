using Microsoft.EntityFrameworkCore.Migrations;

namespace UploadFilesServer.Migrations
{
    public partial class file_table_added_filetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");
        }
    }
}
