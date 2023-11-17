using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TARpe21ShopVaitmaa.Data.Migrations
{
    public partial class fileforcars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "FilesToApi",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilesToApi_CarId",
                table: "FilesToApi",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilesToApi_Cars_CarId",
                table: "FilesToApi",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilesToApi_Cars_CarId",
                table: "FilesToApi");

            migrationBuilder.DropIndex(
                name: "IX_FilesToApi_CarId",
                table: "FilesToApi");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "FilesToApi");
        }
    }
}
