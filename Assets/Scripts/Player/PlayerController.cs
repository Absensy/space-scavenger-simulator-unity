using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float WSForce = 150f; //  Forward and backward force
    public float ADForce = 150f; // Power left and right
    public float turnForce = 100f; // Power turn ship
    

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze Rotation on X and Z
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
    }

    void FixedUpdate() {
        float moveForward = Input.GetAxis("Vertical"); // W, S
        float moveSide = Input.GetAxis("Horizontal"); // A, D

        float turn = 0; // Speed turn
        // Turn  key A and D 
        if (Input.GetKey(KeyCode.A)) turn = -10f;
        if (Input.GetKey(KeyCode.D)) turn = 10f;

        // Movement forward  
        rb.AddRelativeForce(Vector3.forward * moveForward * WSForce * Time.fixedDeltaTime);
        // Movement left and right
        rb.AddRelativeForce(Vector3.right * moveSide * ADForce * Time.fixedDeltaTime);
        // Turn force
        rb.AddRelativeTorque(Vector3.up * turn * turnForce * Time.fixedDeltaTime);
    
    }
}
