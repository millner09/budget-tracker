using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class BudgetTrackerContext : IdentityDbContext
    {
        public BudgetTrackerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<PlannedExpense> PlannedExpenses { get; set; }
        public DbSet<PlannedIncome> PlannedIncomes { get; set; }
        public DbSet<MonthlyBudget> MonthlyBudgets { get; set; }
    }
}