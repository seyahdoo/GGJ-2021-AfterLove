using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    public Joypad[] joypads;
    private void Update() {
        foreach (var joypad in joypads) {
            if (joypad._unityInputEnabled) {
                var horizontalInput = Input.GetAxis(joypad.horizontal);
                var verticalInput = Input.GetAxis(joypad.vertical);
                var unityInput = new Vector2(horizontalInput, verticalInput);
                joypad.input = unityInput.magnitude > joypad.joypadOverallDeadzone ? unityInput : Vector2.zero;
                foreach (var keyCode in joypad.keys) {
                    if (Input.GetKeyDown(keyCode)) {
                        if (joypad._pressedKeys == 0) {
                            joypad._pressedKeys++;
                            if (joypad.tapEnabled) {
                                joypad.tapped = true;
                            }
                        }
                    }
                    if (Input.GetKeyUp(keyCode)) joypad._pressedKeys--;
                    joypad._pressedKeys = Mathf.Clamp(joypad._pressedKeys, 0, 4);
                }
            }
        }
    }
}
