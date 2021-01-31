using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    public Transform followee;
    public Transform secondFollowee;
    public float lerp = 0f;
    private void OnEnable() {
        transform.position = followee.position;
    }
    private void Update() {
        transform.position = Vector3.Lerp(transform.position,
            Vector3.Lerp((secondFollowee.position + followee.position)/2f, followee.position, lerp), .1f);
    }
}
