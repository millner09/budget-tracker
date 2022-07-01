using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class correctedmonthlybudgetrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedExpenses_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannedIncomes_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedIncomes");

            migrationBuilder.AlterColumn<Guid>(
                name: "MonthlyBudgetId",
                table: "PlannedIncomes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MonthlyBudgetId",
                table: "PlannedExpenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedExpenses_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedExpenses",
                column: "MonthlyBudgetId",
                principalTable: "MonthlyBudgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedIncomes_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedIncomes",
                column: "MonthlyBudgetId",
                principalTable: "MonthlyBudgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedExpenses_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannedIncomes_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedIncomes");

            migrationBuilder.AlterColumn<Guid>(
                name: "MonthlyBudgetId",
                table: "PlannedIncomes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "MonthlyBudgetId",
                table: "PlannedExpenses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedExpenses_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedExpenses",
                column: "MonthlyBudgetId",
                principalTable: "MonthlyBudgets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedIncomes_MonthlyBudgets_MonthlyBudgetId",
                table: "PlannedIncomes",
                column: "MonthlyBudgetId",
                principalTable: "MonthlyBudgets",
                principalColumn: "Id");
        }
    }
}
