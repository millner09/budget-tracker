using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
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
        public DateTime MonthlyBudgetDate { get; private set; }

        public void SetMonthlyBudgetDate(DateTime date)
        {
            YearMonth = date.ToString("yyyy-MM");
            MonthlyBudgetDate = date;
        }
    }

    public class PlannedExpense
    {
        public Guid Id { get; set; }
        public MonthlyBudget MonthlyBudget { get; set; }
        public Guid MonthlyBudgetId { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public decimal PlannedAmount { get; set; }
    }

    public class PlannedIncome
    {
        public Guid Id { get; set; }
        public MonthlyBudget MonthlyBudget { get; set; }
        public Guid MonthlyBudgetId { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public decimal PlannedAmount { get; set; }
    }
}
