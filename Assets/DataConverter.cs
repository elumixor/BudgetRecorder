using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using UnityEngine;

public static class DataConverter {
    public static string ToString(List<Category> categories, Category gains) {
        var c = new List<Category>(categories) {gains};

        return c.Count + "\n"
                       + string.Join("\n", c
                           .Select(c1 => c1.name + ";#" + ColorUtility.ToHtmlStringRGBA(c1.color) + ";" + c1.expenses.Count + (c1.expenses.Count > 0 ? "\n" : "")
                                         + string.Join("\n", c1.expenses.Select(e => e.date.ToBinary() + ";" + e.amount))));
    }

    public static string ToReadable(List<Category> categories, Category gains) =>
        string.Join("\n", new List<Category>(categories) {gains}
            .SelectMany(c1 => c1.expenses).Select(e => e.date + ";" + e.amount + ";" + e.category.name));


    public static (List<Category> categories, Category gains) ParseString(string getString) {
        var lines = getString.Split('\n');
        var categoriesCount = int.Parse(lines[0]);
        var c = new List<Category>(categoriesCount);

        Debug.Log(getString);
        
        var l = 0;
        for (var i = 0; i < categoriesCount; i++) {
            l++;
            var line = lines[l].Split(';');
            var name = line[0];
            foreach (var s in line) {
                Debug.Log(s);
            }
            ColorUtility.TryParseHtmlString(line[1], out var color);
            var expenseCount = int.Parse(line[2]);
            var expenses = new List<Expense>(expenseCount);
            for (var j = 0; j < expenseCount; j++) {
                l++;
                var expLine = lines[l].Split(';');
                var date = DateTime.FromBinary(long.Parse(expLine[0]));
                var amount = int.Parse(expLine[1]);

                expenses.Add(new Expense(amount, date));
            }

            var category = new Category(name, color, expenses.ToArray());
            c.Add(category);
            expenses.ForEach(e => e.category = category);
        }

        var categories = c.GetRange(0, categoriesCount - 1);
        var gains = c[c.Count - 1];

        return (categories, gains);
    }
}