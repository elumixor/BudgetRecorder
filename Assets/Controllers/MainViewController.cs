using System.Collections.Generic;
using System.IO;
using System.Linq;
using Components;
using Domain;
using Plugins.NativeShare;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers {
    /// <summary>
    /// Responsible for interaction logic on main application view
    /// </summary>
    public class MainViewController : MonoBehaviour {
        [SerializeField] private InputComponent amountInput;
        [SerializeField] private InputComponent categoryInput;

        [SerializeField] private Button newCategoryButton;
        [SerializeField] private Button gainsButton;

        [SerializeField] private DataController dataController;
        [SerializeField] private StackViewController stackViewController;

        public CategoryComponent categoryComponentPrefab;


        private int CurrentAmount => string.IsNullOrEmpty(amountInput.Text) ? 0 : int.Parse(amountInput.Text);
        private string CurrentCategoryName => categoryInput.Text;


        public void OnAmountInputDone() {
            categoryInput.Focus();
        }

        public void OnAmountInputChanged() {
            gainsButton.interactable = CurrentAmount > 0;
            newCategoryButton.interactable = !string.IsNullOrEmpty(CurrentCategoryName);
        }

        public void OnCategoryInputChanged() {
            newCategoryButton.interactable = !string.IsNullOrEmpty(CurrentCategoryName);
            stackViewController.ReorderBy<CategoryComponent>(RelevancyOrdering);
        }

        public void OnGainButtonPressed() {
            dataController.AddGain(CurrentAmount);
            ClearInputs();
        }

        public void OnCategoryPressed(Category category) {
            if (CurrentAmount <= 0) return;

            dataController.AddExpense(category, CurrentAmount);
            ClearInputs();
        }

        public void OnNewCategoryPressed() {
            if (CurrentAmount > 0) {
                if (dataController.Categories.Exists(c => c.name == CurrentCategoryName)) {
                    PromptController.Prompt(PromptController.Type.ConfirmCancel, o => {
                            if (!(bool) o) return;

                            dataController.AddExpense(dataController.Categories.First(c => c.name == CurrentCategoryName),
                                CurrentAmount);
                            ClearInputs();
                        }, "Category " + CurrentCategoryName + " already exists. Add expense to that category?");
                } else {
                    var category = dataController.CreateCategory(CurrentCategoryName, CurrentAmount);
                    var categoryComponent = Instantiate(categoryComponentPrefab);
                    categoryComponent.Category = category;

                    stackViewController.AddElement(categoryComponent);
                    stackViewController.ReorderBy<CategoryComponent>(DateOrdering);

                    ClearInputs();
                }
            } else if (!dataController.Categories.Exists(c => c.name == CurrentCategoryName)) {
                var category = dataController.CreateCategory(CurrentCategoryName);
                var categoryComponent = Instantiate(categoryComponentPrefab);
                categoryComponent.Category = category;

                stackViewController.AddElement(categoryComponent);
                stackViewController.ReorderBy<CategoryComponent>(DateOrdering);

                ClearInputs();
            }
        }

        private static IEnumerable<int> DateOrdering(IEnumerable<CategoryComponent> components) {
            components = components.ToList();
            // todo: order by date?
            return Enumerable.Range(0, components.Count());
        }

        private IEnumerable<int> RelevancyOrdering(IEnumerable<CategoryComponent> components) {
            components = components.ToList();
            var str = CurrentCategoryName;
            // todo: order by string relevancy
            return Enumerable.Range(0, components.Count());
        }

        private void ClearInputs() {
            amountInput.Clear();
            categoryInput.Clear();
        }

        public void Share() {
            var dataString = dataController.DataString;
            Debug.Log(dataString);
            new NativeShare().SetText(dataString).Share();
        }

        public void ClearExpenses() {
            PromptController.Prompt(PromptController.Type.ConfirmCancel, o => {
                if ((bool) o)
                    dataController.ClearExpenses();
            }, "Are you sure you want to clear all expenses");
        }

        private void Start() {
            OnAmountInputChanged();
            OnCategoryInputChanged();

            foreach (var category in dataController.Categories) {
                var categoryComponent = Instantiate(categoryComponentPrefab);
                categoryComponent.Category = category;
                stackViewController.AddElement(categoryComponent);
            }

            stackViewController.ReorderBy<CategoryComponent>(DateOrdering);
        }
    }
}