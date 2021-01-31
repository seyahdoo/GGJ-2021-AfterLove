using System;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
    public float magnetStrength = 10f;
    public float effectiveRange = 5f;
    private static List<Magnet> _magnets = new List<Magnet>();
    private Rigidbody _rb;
    private void OnEnable() {
        _magnets.Add(this);
    }
    private void OnDisable() {
        _magnets.Remove(this);
    }
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        var pos = transform.position;
        for (int i = 0; i < _magnets.Count; i++) {
            var otherMagnet = _magnets[i];
            if (otherMagnet == this) continue;
            var otherPos = otherMagnet.transform.position;
            var distance = Vector3.Distance(pos, otherPos);
            if (distance > effectiveRange + otherMagnet.effectiveRange) continue;
            var strength = magnetStrength * otherMagnet.magnetStrength;
            var force = strength / distance;
            _rb.AddForce((otherPos - pos).normalized * force);
        }
    }
}
