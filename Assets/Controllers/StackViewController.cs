using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers {
    public class StackViewController : MonoBehaviour {
        [SerializeField] private float elementHeight;
        public List<Transform> children = new List<Transform>();

        public void AddElement<T>(T element) where T : Component {
            var tr = element.transform;
            children.Add(tr);
            tr.SetParent(transform, false);
            SetPosition(tr, children.Count);
            var sd = transform.GetComponent<RectTransform>().sizeDelta;
            sd.y = elementHeight * children.Count;
            transform.GetComponent<RectTransform>().sizeDelta = sd;
        }

        private void SetPosition(Component child, int index) {
            var top = -index * elementHeight;
            var oldPos = child.GetComponent<RectTransform>().anchoredPosition;
            oldPos.y = top;
            child.GetComponent<RectTransform>().anchoredPosition = oldPos;
        }

        public void ReorderBy<T>(Func<IEnumerable<T>, IEnumerable<int>> ordering) {
            var indices = ordering.Invoke(children.Select(c => c.GetComponent<T>())).ToList();

            for (var i = 0; i < indices.Count; i++) {
                var element = children[i];
                var index = indices[i];

                SetPosition(element, index);
            }
        }

        public void UpdateOrder() {
            for (var i = 0; i < children.Count; i++) SetPosition(children[i], i);
        }
    }
}