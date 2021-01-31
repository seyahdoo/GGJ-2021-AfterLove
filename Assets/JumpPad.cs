using UnityEngine;
public class JumpPad : MonoBehaviour {
    public Vector3 directionForce = Vector3.up * 10f;
    private void OnCollisionEnter(Collision other) {
        var worldDirection = transform.TransformDirection(directionForce);
        other.rigidbody.velocity = worldDirection;
    }
}
