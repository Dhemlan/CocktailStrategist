using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailStrategist.Migrations
{
    /// <inheritdoc />
    public partial class removedMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientUsage_Measurement_Measurementid",
                table: "IngredientUsage");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropIndex(
                name: "IX_IngredientUsage_Measurementid",
                table: "IngredientUsage");

            migrationBuilder.DropColumn(
                name: "Measurementid",
                table: "IngredientUsage");

            migrationBuilder.AddColumn<string>(
                name: "Measurement",
                table: "IngredientUsage",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Measurement",
                table: "IngredientUsage");

            migrationBuilder.AddColumn<Guid>(
                name: "Measurementid",
                table: "IngredientUsage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientUsage_Measurementid",
                table: "IngredientUsage",
                column: "Measurementid");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientUsage_Measurement_Measurementid",
                table: "IngredientUsage",
                column: "Measurementid",
                principalTable: "Measurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
