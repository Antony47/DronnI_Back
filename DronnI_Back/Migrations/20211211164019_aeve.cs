using Microsoft.EntityFrameworkCore.Migrations;

namespace DronnI_Back.Migrations
{
    public partial class aeve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Drones_DroneId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_DroneId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DroneId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Drones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drones_CategoryId",
                table: "Drones",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_Category_CategoryId",
                table: "Drones",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_Category_CategoryId",
                table: "Drones");

            migrationBuilder.DropIndex(
                name: "IX_Drones_CategoryId",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Drones");

            migrationBuilder.AddColumn<int>(
                name: "DroneId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_DroneId",
                table: "Category",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Drones_DroneId",
                table: "Category",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
