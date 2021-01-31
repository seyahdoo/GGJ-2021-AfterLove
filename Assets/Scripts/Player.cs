using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Joypad joypad;
    public Rigidbody rb;
    public float force;
    public GameObject[] playerComplexes;
    public Transform player3;
    private float _wontMergeUntil;
    private void OnEnable() {
        _wontMergeUntil = Time.time + .5f;

    }
    private void FixedUpdate() {
        var input = new Vector3(joypad.input.x, 0f, joypad.input.y);
        rb.AddForce(input * force);
    }
    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player2")) {
            if (Time.time > _wontMergeUntil) {
                //merge
                // other.gameObject.SetActive(false);
                // gameObject.SetActive(false);
                playerComplexes[0].SetActive(false);
                playerComplexes[1].SetActive(false);
                playerComplexes[2].SetActive(true);
                player3.position = (transform.position + other.transform.position) / 2;
            }
        }
    }
}
