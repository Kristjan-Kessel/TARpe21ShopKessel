using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TARpe21ShopVaitmaa.Data.Migrations
{
    public partial class indx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DimensionID",
                table: "Dimension",
                newName: "DimensionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DimensionId",
                table: "Dimension",
                newName: "DimensionID");
        }
    }
}
