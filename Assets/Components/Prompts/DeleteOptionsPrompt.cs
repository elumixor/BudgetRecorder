namespace Components.Prompts {
    public class DeleteOptionsPrompt : PromptComponent {
        public enum Type {
            Move,
            Delete,
            Cancel
        }

        public void OnMove() {
            Return(Type.Move);
        }

        public void OnDelete() {
            Return(Type.Delete);
        }

        public void OnCancel() {
            Return(Type.Cancel);
        }
    }
}