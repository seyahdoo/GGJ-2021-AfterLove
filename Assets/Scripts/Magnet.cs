using System;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
    public float magnetStrength = 10f;
    public float effectiveRange = 5f;
    public float disableMagnetUntil;
    private static List<Magnet> _magnets = new List<Magnet>();
    private Rigidbody _rb;
    private void OnEnable() {
        _magnets.Add(this);
        disableMagnetUntil = Time.time;
    }
    private void OnDisable() {
        _magnets.Remove(this);
    }
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        if (_rb == null) return;
        if (disableMagnetUntil > Time.time) return;
        var pos = transform.position;
        for (int i = 0; i < _magnets.Count; i++) {
            var otherMagnet = _magnets[i];
            if (otherMagnet == this) continue;
            if (otherMagnet.disableMagnetUntil > Time.time) return;
            var otherPos = otherMagnet.transform.position;
            var distance = Vector3.Distance(pos, otherPos);
            if (distance > effectiveRange + otherMagnet.effectiveRange) continue;
            var strength = magnetStrength * otherMagnet.magnetStrength;
            var force = strength / distance;
            _rb.AddForce((otherPos - pos).normalized * force);
        }
    }
}
