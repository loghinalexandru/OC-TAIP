using Microsoft.EntityFrameworkCore.Migrations;

namespace AccelerometerStorage.Persistance.EntityFramework.Migrations
{
    public partial class AddFileTypeToDataFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileType",
                table: "DataFiles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "DataFiles");
        }
    }
}
