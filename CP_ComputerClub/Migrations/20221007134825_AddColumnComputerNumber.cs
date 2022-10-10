using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP_ComputerClub.Migrations
{
    public partial class AddColumnComputerNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeaturesPC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VideoCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CPU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RAM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Monitor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mouse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Keyboard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Headphones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesPC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePCId = table.Column<int>(type: "int", nullable: false),
                    IsBusy = table.Column<bool>(type: "bit", nullable: false),
                    IsBroken = table.Column<bool>(type: "bit", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeEnd = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Computers__TypeP__440B1D61",
                        column: x => x.TypePCId,
                        principalTable: "FeaturesPC",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerNumber = table.Column<int>(type: "int", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalSum = table.Column<decimal>(type: "money", nullable: false),
                    TypePCId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK__History__TypePCI__4AB81AF0",
                        column: x => x.TypePCId,
                        principalTable: "FeaturesPC",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computers_TypePCId",
                table: "Computers",
                column: "TypePCId");

            migrationBuilder.CreateIndex(
                name: "IX_History_TypePCId",
                table: "History",
                column: "TypePCId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "FeaturesPC");
        }
    }
}
