using TMPro;
using UnityEngine;

namespace Components.Prompts {
    public class ConfirmCancelPrompt : PromptComponent {
        [SerializeField] private TextMeshProUGUI text;
        
        protected override void SetMessage(string message) {
            text.text = message;
        }

        public void OnConfirm() {
            Return(true);
        }

        public void OnCancel() {
            Return(false);
        }
    }
}