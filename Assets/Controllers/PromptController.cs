using System;
using Components;
using Components.Prompts;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers {
    /// <summary>
    /// Responsible for controlling prompts
    /// </summary>
    public class PromptController : MonoBehaviour {
        public enum Type {
            CategoryOptions,
            ConfirmCancel,
            DeleteOptions,
            MoveCategory,
            RenameCategory
        }

        [SerializeField] private Image background;
        private PromptComponent shownPrompt;

        [SerializeField] private CategoryOptionsPrompt categoryOptionsPrompt;
        [SerializeField] private ConfirmCancelPrompt confirmCancelPrompt;
        [SerializeField] private DeleteOptionsPrompt deleteOptionsPrompt;
        [SerializeField] private MoveCategoryPrompt moveCategoryPrompt;
        [SerializeField] private RenameCategoryPrompt renameCategoryPrompt;
        
        private static PromptController instance;

        private void Awake() {
            instance = this;
        }

        public static void Prompt(Type type, Action<object> onCompleted, string message = null) {
            instance.background.gameObject.SetActive(true);
            switch (type) {
                case Type.CategoryOptions:
                    instance.categoryOptionsPrompt.Show(onCompleted, message);
                    break;
                case Type.ConfirmCancel:
                    instance.confirmCancelPrompt.Show(onCompleted, message);
                    break;
                case Type.DeleteOptions:
                    instance.deleteOptionsPrompt.Show(onCompleted, message);
                    break;
                case Type.MoveCategory:
                    instance.moveCategoryPrompt.Show(onCompleted, message);
                    break;
                case Type.RenameCategory:
                    instance.renameCategoryPrompt.Show(onCompleted, message);
                    break;
                default: break;
            }
        }

        public static void HidePrompt() {
            instance.background.gameObject.SetActive(false);
        }
    }
}