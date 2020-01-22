using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shared {
    public class MouseDownHandler : MonoBehaviour, IPointerDownHandler {
        [SerializeField] private UnityEvent onMouseDown;
        
        public void OnPointerDown (PointerEventData eventData) {
            onMouseDown.Invoke();    
        }
    }
}