using UnityEngine;
public class JumpPad : MonoBehaviour {
    public Vector3 directionForce = Vector3.up * 10f;
    public Animator animator;
    private static readonly int Activate = Animator.StringToHash("activate");
    private void OnCollisionEnter(Collision other) {
        var worldDirection = transform.TransformDirection(directionForce);
        if (other.rigidbody != null) {
            other.rigidbody.velocity = worldDirection;
            animator.SetTrigger(Activate);
        }
    }
}
