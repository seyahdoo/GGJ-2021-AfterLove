using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour {
    public Joypad joypad;
    public Rigidbody rb;
    public float force;
    public GameObject[] playerComplexes;
    public Transform player3;
    public float maxSpeed = 10f;
    private float _wontMergeUntil;
    private void OnEnable() {
        _wontMergeUntil = Time.time + .5f;
    }
    private void FixedUpdate() {
        var input = new Vector3(joypad.input.x, 0f, joypad.input.y);
        if (rb.velocity.magnitude < maxSpeed || Vector3.Angle(rb.velocity.normalized, input.normalized) > 135f) {
            rb.AddForce(input * force);
        }
        if (joypad.tapped) {
            joypad.tapped = false;
            joypad.tapEnabled = false;
            rb.AddForce(Vector3.up * 4f, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision other) {
        joypad.tapEnabled = true;
        if (other.collider.CompareTag("Player2")) {
            if (Time.time > _wontMergeUntil) {
                //merge
                // other.gameObject.SetActive(false);
                // gameObject.SetActive(false);
                player3.position = (transform.position + other.transform.position) / 2;
                playerComplexes[0].SetActive(false);
                playerComplexes[1].SetActive(false);
                playerComplexes[2].SetActive(true);
            }
        }
    }
    private void OnCollisionStay(Collision other) {
        joypad.tapEnabled = true;
    }
}
