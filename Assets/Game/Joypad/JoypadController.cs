using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mobge {
    public class JoypadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        public Joypad Player1Joypad;
        public Joypad Player2Joypad;
        private void Awake() {
            Player1Joypad.Init();
            Player2Joypad.Init();
        }
        public void OnPointerDown(PointerEventData eventData) {
            if (eventData.position.y < Screen.height / 2f) {
                Player1Joypad.OnPointerDown(eventData);
            }
            else {
                Player2Joypad.OnPointerDown(eventData);
            }
        }
        void IDragHandler.OnDrag(PointerEventData eventData) {
            Player1Joypad.OnDrag(eventData);
            Player2Joypad.OnDrag(eventData);
        }
        public void OnPointerUp(PointerEventData eventData) {
            Player1Joypad.OnPointerUp(eventData);
            Player2Joypad.OnPointerUp(eventData);
        }
        [Serializable]
        public class Joypad {
            public RectTransform joyPad;
            public float touchHorizontalDeadzonePercentage = 0f;
            public float touchVerticalDeadzonePercentage = 0f;
            public float joypadOverallDeadzone = .1f;
            public float sensitivity = .05f;
            public bool normalizeInput = true;
            public Animator joyPadAnimator;
            public string animatorHorizontalParamName = "blendX";
            public string animatorVerticalParamName = "blendY";
            private const int NON_POINTER = -2973642;
            private int _startPointerId;
            private Vector2 _dragDirection;
            private Vector2 _startPoint;
            private Vector2 _input;
            public void Init() {
                _startPointerId = NON_POINTER;
                joyPad.gameObject.SetActive(false);
            }
            public void OnPointerDown(PointerEventData eventData) {
                if (_startPointerId == NON_POINTER) {
                    joyPad.gameObject.SetActive(true);
                    _startPointerId = eventData.pointerId;
                    _startPoint = eventData.position;
                    joyPad.position = _startPoint;
                    UpdateJoypadGraphic(Vector2.zero);
                }
            }
            public void OnDrag(PointerEventData eventData) {
                if (_startPointerId == eventData.pointerId) {
                    var diff = eventData.position - _startPoint;
                    if (Mathf.Abs(diff.x) / Screen.width < touchHorizontalDeadzonePercentage) diff.x = 0;
                    if (Mathf.Abs(diff.y) / Screen.height < touchVerticalDeadzonePercentage) diff.y = 0;
                    diff /= Screen.width * sensitivity;
                    if (normalizeInput) diff = diff.normalized;
                    _input = Vector2.ClampMagnitude(diff, 1f);
                    _input = _input.magnitude > joypadOverallDeadzone ? _input : Vector2.zero;
                    UpdateJoypadGraphic(_input);
                }
            }
            public void OnPointerUp(PointerEventData eventData) {
                if (eventData.pointerId == _startPointerId) {
                    joyPad.gameObject.SetActive(false);
                    _startPointerId = NON_POINTER;
                    _input = Vector2.zero;
                }
            }
            private void UpdateJoypadGraphic(Vector2 position) {
                joyPadAnimator.SetFloat(animatorHorizontalParamName, position.x);
                joyPadAnimator.SetFloat(animatorVerticalParamName, position.y);
            }
        }
    }
}