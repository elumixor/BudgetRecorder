using UnityEngine;
using UnityEngine.UI;

namespace Components.Prompts {
    public class CategoryOptionsPrompt : PromptComponent {
        public enum SelectedAction {
            Rename,
            ClearExpense,
            Delete,
            Cancel
        }

        public void OnRename() {
            Return(SelectedAction.Rename);
        }

        public void OnClearExpense() {
            Return(SelectedAction.ClearExpense);
        }

        public void OnDelete() {
            Return(SelectedAction.Delete);
        }

        public void OnCancel() {
            Return(SelectedAction.Cancel);
        }
    }
}