using System;
using Controllers;
using UnityEngine;

namespace Components {
    public class PromptComponent : MonoBehaviour {
        private Action<object> onCompleted;

        private static Transform parent;

        protected virtual void SetMessage(string message) {}

        public void Show(Action<object> onCompleted, string message) {
            gameObject.SetActive(true);
            this.onCompleted = onCompleted;
            SetMessage(message);
        }

        protected void Return(object value = null) {
            gameObject.SetActive(false);
            PromptController.HidePrompt();
            onCompleted(value);
        }
    }
}