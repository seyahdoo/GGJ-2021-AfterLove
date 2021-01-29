using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mobge {
    public class Joypad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        public static Vector2 currentInput;
        public static event Action<PointerEventData> onPointerUp;
        public static event Action<PointerEventData> onPointerStay;
        public static event Action<PointerEventData> onPointerDown;

        public RectTransform joyPad;
        public float touchHorizontalDeadzonePercentage = 0.01f;
        public float touchVerticalDeadzonePercentage = 0.01f;
        public float unityInputDeadzone = .1f;
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
        private bool _unityInputEnabled;
        private void Awake() {
            _startPointerId = NON_POINTER;
        }
        public void OnPointerDown(PointerEventData eventData) {
            if (_startPointerId == NON_POINTER) {
                onPointerDown?.Invoke(eventData);
                _unityInputEnabled = false;
                joyPad.gameObject.SetActive(true);
                _startPointerId = eventData.pointerId;
                _startPoint = eventData.position;
                joyPad.position = _startPoint;
            }
        }
        void IDragHandler.OnDrag(PointerEventData eventData) {
            if (_startPointerId == eventData.pointerId) {
                onPointerStay?.Invoke(eventData);
                var diff = eventData.position - _startPoint;
                if (Mathf.Abs(diff.x) / Screen.width < touchHorizontalDeadzonePercentage) diff.x = 0;
                if (Mathf.Abs(diff.y) / Screen.height < touchVerticalDeadzonePercentage) diff.y = 0;
                diff /= Screen.width * sensitivity;
                if (normalizeInput) diff = diff.normalized;
                _input = Vector2.ClampMagnitude(diff, 1f);
            }
        }
        public void OnPointerUp(PointerEventData eventData) {
            if (eventData.pointerId == _startPointerId) {
                onPointerUp?.Invoke(eventData);
                _unityInputEnabled = true;
                joyPad.gameObject.SetActive(false);
                _startPointerId = NON_POINTER;
                _input = Vector2.zero;
            }
        }
        private void Update() {
            if (_unityInputEnabled) {
                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");
                var unityInput = new Vector2(horizontal, vertical);
                _input = unityInput.magnitude > unityInputDeadzone ? unityInput : Vector2.zero;
                if (Input.GetKeyUp(KeyCode.Space))   onPointerDown?.Invoke(null);
                if (Input.GetKey(KeyCode.Space))     onPointerStay?.Invoke(null);
                if (Input.GetKeyDown(KeyCode.Space)) onPointerUp?.Invoke(null);
            }
            _input = _input.magnitude > joypadOverallDeadzone ? _input : Vector2.zero;
            currentInput = _input;
            joyPadAnimator.SetFloat(animatorHorizontalParamName, _input.x);
            joyPadAnimator.SetFloat(animatorVerticalParamName, _input.y);
        }
    }
}