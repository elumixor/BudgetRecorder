using System.Collections.Generic;
using System.Linq;
using Components.Prompts;
using Controllers;
using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components {
    public class CategoryComponent : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image background;

        private Category category;

        public Category Category {
            get => category;
            set {
                category = value;
                text.text = category.name;
                background.color = category.color;
            }
        }

        private MainViewController mainViewController;
        private DataController dataController;

        private void Awake() {
            mainViewController = FindObjectOfType<MainViewController>();
            dataController = FindObjectOfType<DataController>();
        }

        public void OnClick() {
            mainViewController.OnCategoryPressed(category);
        }

        private void Remove() {
            dataController.Categories.Remove(category);

            foreach (var svc in FindObjectsOfType<StackViewController>()) {
                var toRemove = svc.children.FirstOrDefault(c =>
                    c.GetComponent<CategoryComponent>().category == category ||
                    c.GetComponent<CategorySelectorComponent>().Category == category);
                if (toRemove == null) continue;

                svc.children.Remove(transform);
                svc.UpdateOrder();
            }

            Destroy(gameObject);
        }

        public void OnContext() {
            PromptController.Prompt(PromptController.Type.CategoryOptions, o => {
                var res = (CategoryOptionsPrompt.SelectedAction) o;

                switch (res) {
                    case CategoryOptionsPrompt.SelectedAction.Rename:
                        PromptController.Prompt(PromptController.Type.RenameCategory, o1 => {
                            var newName = (string) o1;
                            if (newName == category.name) return;


                            if (dataController.Categories.Any(c => c.name == newName)) {
                                PromptController.Prompt(PromptController.Type.ConfirmCancel, o2 => {
                                        if (!(bool) o2) return;

                                        var target = dataController.Categories.First(c => c.name == newName);

                                        foreach (var expense in category.expenses)
                                            target.expenses.Add(new Expense(expense.amount, expense.date, target));

                                        Remove();
                                    }, "Category with name " + newName + " exists. Do you want to move expenses into that category?");
                            } else {
                                category.name = newName;
                            }
                        }, category.name);
                        break;
                    case CategoryOptionsPrompt.SelectedAction.ClearExpense:
                        PromptController.Prompt(PromptController.Type.ConfirmCancel, o1 => {
                                if (!(bool) o1) return;

                                category.expenses = new List<Expense>();
                            }, "Are you sure to clear all expenses for " + category.name + "?");
                        break;
                    case CategoryOptionsPrompt.SelectedAction.Delete:
                        if (category.expenses.Count > 0) {
                            PromptController.Prompt(PromptController.Type.DeleteOptions, o1 => {
                                var res2 = (DeleteOptionsPrompt.Type) o1;

                                switch (res2) {
                                    case DeleteOptionsPrompt.Type.Delete:
                                        Remove();
                                        break;
                                    case DeleteOptionsPrompt.Type.Move:
                                        dataController.Categories.Remove(category);

                                        PromptController.Prompt(PromptController.Type.MoveCategory, o2 => {
                                            dataController.Categories.Add(category);
                                            
                                            if (o2 == null) return;

                                            var target = (Category) o2;

                                            foreach (var expense in category.expenses)
                                                target.expenses.Add(new Expense(expense.amount, expense.date, target));

                                            Remove();
                                        });
                                        break;
                                    default:
                                        break;
                                }
                            });
                        } else {
                            PromptController.Prompt(PromptController.Type.ConfirmCancel, o1 => {
                                    if ((bool) o1) Remove();
                                }, "Are you sure to delete category " + category.name + "?");
                        }

                        break;
                    default:
                        break;
                }
            });
        }
    }
}