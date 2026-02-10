using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Movement Settings")]
    public float moveForce = 300f;  // Speed forward/backward
    public float turnSpeed = 100f;  // Fixed rotation speed (Degrees per second)
    public float maxSpeed = 10f;    // Max speed ship

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();

        // Physics setup for stable driving
        rb.useGravity = true;
        rb.linearDamping = 2f;    // Air resistance/Braking

        // Set angular damping to 0 for manual velocity control
        rb.angularDamping = 0f;

        // Freeze rotation X and Z to stay upright
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate() {
        float moveInput = Input.GetAxis("Vertical");   // W, S
        float turnInput = Input.GetAxis("Horizontal"); // A, D

        // Forward/Backward movement
        if (rb.linearVelocity.magnitude < maxSpeed) {
            rb.AddRelativeForce(Vector3.forward * moveInput * moveForce);
        }
        float turnAmount = turnInput * turnSpeed;

        // This makes rotation instant and perfectly constant
        rb.angularVelocity = new Vector3(0, turnAmount * Mathf.Deg2Rad, 0);
    }
}