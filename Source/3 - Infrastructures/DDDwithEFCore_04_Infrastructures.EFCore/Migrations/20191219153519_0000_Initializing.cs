using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Migrations
{
    public partial class _0000_Initializing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DDDwithEFCore_03_ApplicationCore.DomainModels.Peoples",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDDwithEFCore_03_ApplicationCore.DomainModels.Peoples", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DDDwithEFCore_03_ApplicationCore.DomainModels.Peoples");
        }
    }
}
