using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class PlannedExpensePlannedIncomeMonthlyBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyBudgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartingBalance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyBudgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlannedExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlannedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    MonthlyBudgetId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedExpenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlannedExpenses_MonthlyBudgets_MonthlyBudgetId",
                        column: x => x.MonthlyBudgetId,
                        principalTable: "MonthlyBudgets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlannedIncomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlannedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    MonthlyBudgetId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedIncomes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlannedIncomes_MonthlyBudgets_MonthlyBudgetId",
                        column: x => x.MonthlyBudgetId,
                        principalTable: "MonthlyBudgets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannedExpenses_CategoryId",
                table: "PlannedExpenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedExpenses_MonthlyBudgetId",
                table: "PlannedExpenses",
                column: "MonthlyBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedIncomes_CategoryId",
                table: "PlannedIncomes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedIncomes_MonthlyBudgetId",
                table: "PlannedIncomes",
                column: "MonthlyBudgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannedExpenses");

            migrationBuilder.DropTable(
                name: "PlannedIncomes");

            migrationBuilder.DropTable(
                name: "MonthlyBudgets");
        }
    }
}
