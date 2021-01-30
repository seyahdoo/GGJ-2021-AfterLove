using UnityEngine;
using UnityEngine.EventSystems;

public class Joypad : MonoBehaviour {
    public Vector2 input;
    [SerializeField] private RectTransform joyPad;
    [SerializeField] private float touchHorizontalDeadzonePercentage = 0f;
    [SerializeField] private float touchVerticalDeadzonePercentage = 0f;
    [SerializeField] private float joypadOverallDeadzone = .1f;
    [SerializeField] private float sensitivity = .05f;
    [SerializeField] private bool normalizeInput = true;
    [SerializeField] private Animator joyPadAnimator;
    [SerializeField] private string animatorHorizontalParamName = "blendX";
    [SerializeField] private string animatorVerticalParamName = "blendY";
    private const int NON_POINTER = -2973642;
    private int _startPointerId;
    private Vector2 _startPoint;
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
            input = Vector2.ClampMagnitude(diff, 1f);
            input = input.magnitude > joypadOverallDeadzone ? input : Vector2.zero;
            UpdateJoypadGraphic(input);
        }
    }
    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.pointerId == _startPointerId) {
            joyPad.gameObject.SetActive(false);
            _startPointerId = NON_POINTER;
            input = Vector2.zero;
        }
    }
    private void UpdateJoypadGraphic(Vector2 position) {
        joyPadAnimator.SetFloat(animatorHorizontalParamName, position.x);
        joyPadAnimator.SetFloat(animatorVerticalParamName, position.y);
    }
}