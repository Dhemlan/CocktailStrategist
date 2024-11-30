using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailStrategist.Migrations
{
    /// <inheritdoc />
    public partial class recipeWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientUsages_Recipes_RecipeId",
                table: "IngredientUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Drinks_DrinkId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientUsages",
                table: "IngredientUsages");

            migrationBuilder.DropIndex(
                name: "IX_IngredientUsages_IngredientId",
                table: "IngredientUsages");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "IngredientUsages");

            migrationBuilder.AlterColumn<Guid>(
                name: "DrinkId",
                table: "Recipes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientUsages",
                table: "IngredientUsages",
                columns: new[] { "IngredientId", "Measurement", "Amount" });

            migrationBuilder.CreateTable(
                name: "IngredientUsageRecipe",
                columns: table => new
                {
                    RecipesId = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredientUsagesIngredientId = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredientUsagesMeasurement = table.Column<string>(type: "text", nullable: false),
                    IngredientUsagesAmount = table.Column<decimal>(type: "numeric(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientUsageRecipe", x => new { x.RecipesId, x.IngredientUsagesIngredientId, x.IngredientUsagesMeasurement, x.IngredientUsagesAmount });
                    table.ForeignKey(
                        name: "FK_IngredientUsageRecipe_IngredientUsages_IngredientUsagesIngr~",
                        columns: x => new { x.IngredientUsagesIngredientId, x.IngredientUsagesMeasurement, x.IngredientUsagesAmount },
                        principalTable: "IngredientUsages",
                        principalColumns: new[] { "IngredientId", "Measurement", "Amount" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientUsageRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientUsageRecipe_IngredientUsagesIngredientId_Ingredie~",
                table: "IngredientUsageRecipe",
                columns: new[] { "IngredientUsagesIngredientId", "IngredientUsagesMeasurement", "IngredientUsagesAmount" });

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Drinks_DrinkId",
                table: "Recipes",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Drinks_DrinkId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "IngredientUsageRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientUsages",
                table: "IngredientUsages");

            migrationBuilder.AlterColumn<Guid>(
                name: "DrinkId",
                table: "Recipes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "IngredientUsages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientUsages",
                table: "IngredientUsages",
                columns: new[] { "RecipeId", "IngredientId" });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientUsages_IngredientId",
                table: "IngredientUsages",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientUsages_Recipes_RecipeId",
                table: "IngredientUsages",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Drinks_DrinkId",
                table: "Recipes",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id");
        }
    }
}
