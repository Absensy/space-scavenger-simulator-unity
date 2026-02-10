using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveForce = 300f;  // Speed forward/backward
    public float turnForce = 500f;  // Power turn ship
    public float maxSpeed = 10f;    // Max speed ship

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // For driving on platform
        rb.linearDamping = 2f; // Smooth braking
        rb.angularDamping = 3f; // Smooth braking rotation

        // Freeze rotation X and Z to stay upright
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate() {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Check current speed
        if (rb.linearVelocity.magnitude < maxSpeed) {
            rb.AddRelativeForce(Vector3.forward * moveInput * moveForce);
        }

        // Turn ship
        rb.AddRelativeTorque(Vector3.up * turnInput * turnForce);
    }
}