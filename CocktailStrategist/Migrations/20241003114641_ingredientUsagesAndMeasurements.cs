using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailStrategist.Migrations
{
    /// <inheritdoc />
    public partial class ingredientUsagesAndMeasurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkIngredient");

            migrationBuilder.AddColumn<Guid>(
                name: "IngredientId",
                table: "Drinks",
                type: "uuid",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "IngredientUsage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Measurementid = table.Column<Guid>(type: "uuid", nullable: false),
                    DrinkId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientUsage_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IngredientUsage_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientUsage_Measurement_Measurementid",
                        column: x => x.Measurementid,
                        principalTable: "Measurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_IngredientId",
                table: "Drinks",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientUsage_DrinkId",
                table: "IngredientUsage",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientUsage_IngredientId",
                table: "IngredientUsage",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientUsage_Measurementid",
                table: "IngredientUsage",
                column: "Measurementid");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Ingredients_IngredientId",
                table: "Drinks",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Ingredients_IngredientId",
                table: "Drinks");

            migrationBuilder.DropTable(
                name: "IngredientUsage");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_IngredientId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Drinks");

            migrationBuilder.CreateTable(
                name: "DrinkIngredient",
                columns: table => new
                {
                    DrinksId = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredientsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkIngredient", x => new { x.DrinksId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_DrinkIngredient_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkIngredient_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredient_IngredientsId",
                table: "DrinkIngredient",
                column: "IngredientsId");
        }
    }
}
