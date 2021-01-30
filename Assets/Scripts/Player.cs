using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Joypad joypad;
    public Rigidbody rb;
    public float force;
    private void FixedUpdate() {
        var input = new Vector3(joypad.input.x, 0f, joypad.input.y);
        rb.AddForce(input * force);
    }
}
