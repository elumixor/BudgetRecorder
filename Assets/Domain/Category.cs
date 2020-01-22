using System.Collections.Generic;
using UnityEngine;

namespace Domain {
    public struct Category {
        public string name;
        public Color color;
        public List<Expense> expenses;

        public Category(string name, params Expense[] expenses) {
            this.name = name;
            this.expenses = new List<Expense>(expenses);
            color = Random.ColorHSV(0f, 1f, .8f, .8f, 0.1f, 0.5f);
        }
        
        public Category(string name, Color color, params Expense[] expenses) {
            this.name = name;
            this.expenses = new List<Expense>(expenses);
            this.color = color;
        }

        public static bool operator ==(Category a, Category b) => a.name == b.name;
        public static bool operator !=(Category a, Category b) => !(a == b);
    }
}