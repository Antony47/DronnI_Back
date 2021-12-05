using Microsoft.EntityFrameworkCore.Migrations;

namespace DronnI_Back.Migrations
{
    public partial class Kremeny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Statistics_StatisticId",
                table: "Rents");

            migrationBuilder.AlterColumn<int>(
                name: "StatisticId",
                table: "Rents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Statistics_StatisticId",
                table: "Rents",
                column: "StatisticId",
                principalTable: "Statistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Statistics_StatisticId",
                table: "Rents");

            migrationBuilder.AlterColumn<int>(
                name: "StatisticId",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Statistics_StatisticId",
                table: "Rents",
                column: "StatisticId",
                principalTable: "Statistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
