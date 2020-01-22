using System;

namespace Domain {
    /// <summary>
    /// Single expense
    /// </summary>
    public struct Expense {
        /// <summary>
        /// Expense category
        /// </summary>
        public Category category;

        /// <summary>
        /// When expense was created
        /// </summary>
        public DateTime date;

        /// <summary>
        /// Expense amount
        /// </summary>
        public int amount;

        public Expense(int amount) : this() {
            this.amount = amount;
            date = DateTime.Now;
        }

        public Expense(int amount, DateTime date) : this() {
            this.amount = amount;
            this.date = date;
        }

        public Expense(int amount, DateTime date, Category category) {
            this.amount = amount;
            this.date = date;
            this.category = category;
        }
    }
}