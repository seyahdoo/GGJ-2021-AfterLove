using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mobge {
    public class MultiJoypadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
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
    }
}