using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailStrategist.Migrations
{
    /// <inheritdoc />
    public partial class recipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientUsage_Ingredients_IngredientId",
                table: "IngredientUsage");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientUsage_Recipes_RecipeId",
                table: "IngredientUsage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientUsage",
                table: "IngredientUsage");

            migrationBuilder.DropIndex(
                name: "IX_IngredientUsage_RecipeId",
                table: "IngredientUsage");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IngredientUsage");

            migrationBuilder.RenameTable(
                name: "IngredientUsage",
                newName: "IngredientUsages");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientUsage_IngredientId",
                table: "IngredientUsages",
                newName: "IX_IngredientUsages_IngredientId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecipeId",
                table: "IngredientUsages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "IngredientUsages",
                type: "numeric(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientUsages",
                table: "IngredientUsages",
                columns: new[] { "RecipeId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientUsages_Ingredients_IngredientId",
                table: "IngredientUsages",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientUsages_Recipes_RecipeId",
                table: "IngredientUsages",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientUsages_Ingredients_IngredientId",
                table: "IngredientUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientUsages_Recipes_RecipeId",
                table: "IngredientUsages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientUsages",
                table: "IngredientUsages");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "IngredientUsages");

            migrationBuilder.RenameTable(
                name: "IngredientUsages",
                newName: "IngredientUsage");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientUsages_IngredientId",
                table: "IngredientUsage",
                newName: "IX_IngredientUsage_IngredientId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecipeId",
                table: "IngredientUsage",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "IngredientUsage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientUsage",
                table: "IngredientUsage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientUsage_RecipeId",
                table: "IngredientUsage",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientUsage_Ingredients_IngredientId",
                table: "IngredientUsage",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientUsage_Recipes_RecipeId",
                table: "IngredientUsage",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
