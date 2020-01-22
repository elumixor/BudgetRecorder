using UnityEngine;

namespace Components.Prompts {
    public class RenameCategoryPrompt : PromptComponent{
        [SerializeField] private InputComponent inputComponent;
        
        
        protected override void SetMessage(string message) {
            inputComponent.Text = message;
        }

        public void OnConfirm() {
            Return(inputComponent.Text);    
        }

        public void OnCancel() {
            Return(inputComponent.Text);
        }
    }
}