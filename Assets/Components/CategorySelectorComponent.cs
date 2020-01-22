using Components.Prompts;
using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components {
    public class CategorySelectorComponent : MonoBehaviour {
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

        private MoveCategoryPrompt categoryOptionsPrompt;

        private void Awake() {
            categoryOptionsPrompt = FindObjectOfType<MoveCategoryPrompt>();
        }

        public void OnClick() {
            categoryOptionsPrompt.OnCategorySelected(category);
        }
    }
}