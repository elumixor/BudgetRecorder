using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shared {
    public class ContextMenuHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        [SerializeField] private UnityEvent onSimple;
        [SerializeField] private UnityEvent onContext;

        private const float CONTEXT_THRESHOLD = .5f;

        private float holdStartTime;
        private bool released;

        public void OnPointerDown(PointerEventData eventData) {
            holdStartTime = Time.time;
            released = false;

            IEnumerator CountForContext() {
                while (Time.time - holdStartTime < CONTEXT_THRESHOLD) {
                    if (released) yield break;
                    yield return null;
                }
            
                Context();
            }

            StartCoroutine(CountForContext());
        }


        public void OnPointerUp(PointerEventData eventData) {
            released = true;
        
            if (Time.time - holdStartTime < CONTEXT_THRESHOLD) Simple();
        }

        private void Simple() {
            onSimple.Invoke();
        }

        private void Context() {
            onContext.Invoke();
        }
    }
}