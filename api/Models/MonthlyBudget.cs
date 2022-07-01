using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class MonthlyBudget
    {
        public MonthlyBudget() { }
        public MonthlyBudget(decimal startingBalance, DateTime startDate)
        {
            StartingBalance = startingBalance;
            MonthlyBudgetDate = startDate;
            YearMonth = startDate.ToString("yyyy-MM");
        }
        public Guid Id { get; set; }
        public string YearMonth { get; private set; }
        public decimal StartingBalance { get; set; }
        public List<PlannedExpense> PlannedExpenses { get; set; }
        public List<PlannedIncome> PlannedIncomes { get; set; }
        public DateTime MonthlyBudgetDate { get; set; }
    }

    public class PlannedExpense
    {
        public Guid Id { get; set; }
        public MonthlyBudget MonthlyBudget { get; set; }
        public Guid MonthlyBudgetId { get; set; }
        public Category Category { get; set; }
        public decimal PlannedAmount { get; set; }
    }

    public class PlannedIncome
    {
        public Guid Id { get; set; }
        public MonthlyBudget MonthlyBudget { get; set; }
        public Guid MonthlyBudgetId { get; set; }
        public Category Category { get; set; }
        public decimal PlannedAmount { get; set; }
    }
}