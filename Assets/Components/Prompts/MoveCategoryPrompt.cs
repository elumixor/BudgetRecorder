using System.Collections.Generic;
using Controllers;
using Domain;
using TMPro;
using UnityEngine;

namespace Components.Prompts {
    public class MoveCategoryPrompt : PromptComponent {
        [SerializeField] private StackViewController stackViewController;
        [SerializeField] private DataController dataController;
        [SerializeField] private TextMeshProUGUI selectedCategoryName;
        [SerializeField] private CategorySelectorComponent categorySelectorPrefab;

        private Category selectedCategory;

        private void OnEnable() {
            stackViewController.children.ForEach(c => Destroy(c.gameObject));
            stackViewController.children = new List<Transform>();

            foreach (var category in dataController.Categories) {
                var categoryComponent = Instantiate(categorySelectorPrefab);
                categoryComponent.Category = category;
                stackViewController.AddElement(categoryComponent);
            }

            selectedCategory = dataController.Categories[0];
        }

        public void OnConfirm() {
            Return(selectedCategory);
        }

        public void OnCancel() {
            Return();
        }

        public void OnCategorySelected(Category category) {
            selectedCategory = category;
            selectedCategoryName.text = category.name;
        }
    }
}