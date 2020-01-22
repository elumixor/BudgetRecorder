using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Components {
    public class InputComponent : MonoBehaviour {
        [SerializeField] private GameObject clearButton;
        [SerializeField] private TMPro.TMP_InputField inputField;

        public UnityEvent onDone;
        public UnityEvent onChange;

        private Animator inputAnimator;
        private Animator buttonAnimator;

        private static readonly int Selected = Animator.StringToHash("Selected");
        public string Text {
            get => inputField.text;
            set => inputField.text = value;
        }


        private void Start() {
            inputAnimator = inputField.GetComponent<Animator>();
            buttonAnimator = clearButton.GetComponent<Animator>();
        }

        public void Clear() {
            inputField.text = "";
        }

        public void OnSelected() {
            inputAnimator.SetBool(Selected, true);
            buttonAnimator.SetBool(Selected, true);
        }

        public void OnDeselected() {
            inputAnimator.SetBool(Selected, false);
            buttonAnimator.SetBool(Selected, false);
        }

        public void OnDone() {
            onDone.Invoke();
        }

        public void OnChange() {
            onChange.Invoke();
        }

        public void Focus() {
            inputField.Select();
        }
    }
}