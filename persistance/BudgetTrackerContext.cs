using domain;
using Microsoft.EntityFrameworkCore;

namespace persistance
{
    public class BudgetTrackerContext : DbContext
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