using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using UnityEngine;

namespace Controllers {
    public class DataController : MonoBehaviour {
        private List<Category> categories;

        public Category gains;

        public string DataString => DataConverter.ToReadable(Categories, gains);

        public List<Category> Categories {
            get {
                if (categories == null) LoadData();
                return categories;
            }
        }

        public void AddExpense(Category category, int expenseAmount) {
            var expense = new Expense(expenseAmount) {category = category};
            category.expenses.Add(expense);
            SaveData();
        }

        public void AddGain(int amount) {
            var gain = new Expense(amount);
            gains.expenses.Add(gain);
            SaveData();
        }

        public Category CreateCategory(string categoryName, params int[] expensesAmounts) {
            var category = new Category(categoryName);
            Categories.Add(category);

            foreach (var amount in expensesAmounts) AddExpense(category, amount);

            SaveData();

            return category;
        }

        private void Awake() {
            if (categories == null) LoadData();
        }

        private void LoadData() {
            if (PlayerPrefs.HasKey("data")) {
                try {
                    var (c, g) = DataConverter.ParseString(PlayerPrefs.GetString("data"));
                    categories = c;
                    gains = g;
                }
                catch (Exception e) {
                    Debug.Log(e);
                    categories = new List<Category>();
                    gains = new Category("Gains");
                }
            } else {
                categories = new List<Category>();
                gains = new Category("Gains");
            }
        }

        public void ClearExpenses() {
            for (var i = 0; i < Categories.Count; i++) Categories[i] = new Category(Categories[i].name, Categories[i].color);
            SaveData();
        }

        private void SaveData() {
            PlayerPrefs.SetString("data", DataConverter.ToString(Categories, gains));
        }
    }
}