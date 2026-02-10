using UnityEngine;

public class TractorBeam : MonoBehaviour {
    [Header("Settings")]
    public float pullPower = 300f;
    public float rangeBeam = 25f;
    public LayerMask ignoreLayer;

    [Header("References")]
    public Transform holdPoint;

    private LineRenderer line;
    private GameObject lockedObject;
    private Rigidbody lockedRb;
    private FixedJoint joint; // New: invisible "welding"
    private bool isHolding = false;

    void Start() {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isHolding) SearchTarget();
        if (Input.GetKeyDown(KeyCode.G)) Release();

        if (isHolding && lockedObject != null) {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, lockedObject.transform.position);
        }
        else {
            line.enabled = false;
        }
    }

    void FixedUpdate() {
        if (isHolding && lockedObject != null && joint == null) {
            float distance = Vector3.Distance(lockedObject.transform.position, holdPoint.position);

            if (distance > 0.4f) {
                // Pulling
                Vector3 direction = holdPoint.position - lockedObject.transform.position;
                lockedRb.AddForce(direction.normalized * pullPower);
                lockedRb.linearVelocity *= 0.9f;
            }
            else {
                // LOCK WITHOUT PARENTING (No scale issues!)
                CaptureWithJoint();
            }
        }
    }

    private void SearchTarget() {
        RaycastHit hit;
        if (Physics.Raycast(holdPoint.position, transform.forward, out hit, rangeBeam, ~ignoreLayer)) {
            if (hit.collider.CompareTag("Trash")) {
                lockedObject = hit.collider.gameObject;
                lockedRb = lockedObject.GetComponent<Rigidbody>();
                isHolding = true;
            }
        }
    }

    private void CaptureWithJoint() {
        lockedRb.linearVelocity = Vector3.zero;
        lockedRb.angularVelocity = Vector3.zero;
        lockedObject.transform.position = holdPoint.position;
        lockedObject.transform.rotation = holdPoint.rotation;
        lockedRb.mass = 0.001f;
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = lockedRb;
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;
        Collider col = lockedObject.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    private void Release() {
        if (lockedObject != null) {
            // Destroy welding
            if (joint != null) Destroy(joint);
            lockedRb.mass = 1.0f;
            Collider col = lockedObject.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            lockedRb.AddForce(transform.forward * 5f, ForceMode.Impulse);
            lockedObject = null;
        }
        isHolding = false;
    }
}