using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    public Transform followee;
    private void Update() {
        transform.position = Vector3.Lerp(transform.position, followee.position, .1f);
    }
}
