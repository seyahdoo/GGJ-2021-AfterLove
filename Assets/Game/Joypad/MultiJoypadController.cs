using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mobge {
    public class MultiJoypadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        public Joypad player1Joypad;
        public Joypad player2Joypad;
        private void Awake() {
            player1Joypad.Init();
            player2Joypad.Init();
        }
        public void OnPointerDown(PointerEventData eventData) {
            if (eventData.position.y < Screen.height / 2f) {
                player1Joypad.OnPointerDown(eventData);
            }
            else {
                player2Joypad.OnPointerDown(eventData);
            }
        }
        void IDragHandler.OnDrag(PointerEventData eventData) {
            player1Joypad.OnDrag(eventData);
            player2Joypad.OnDrag(eventData);
        }
        public void OnPointerUp(PointerEventData eventData) {
            player1Joypad.OnPointerUp(eventData);
            player2Joypad.OnPointerUp(eventData);
        }
    }
}