using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccelerometerStorage.Persistance.EntityFramework.Migrations
{
    public partial class RemoveContentFieldFromDataFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "DataFiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "DataFiles",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
