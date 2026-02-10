using UnityEngine;

public class TractorBeam : MonoBehaviour {
    [Header("Settings")]
    public float pullPower = 500f; // Strength of the tractor beam
    public float rangeBeam = 25f;  // Raycast distance
    public LayerMask ignoreLayer;  // Set to 'Player' layer

    [Header("References")]
    public Transform holdPoint;    // The point where trash will be attached

    private LineRenderer line;
    private GameObject lockedObject;
    private Rigidbody lockedRb;
    private bool isHolding = false;
    private bool isAttached = false;

    void Start() {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    void Update() {
        // Activation
        if (Input.GetKeyDown(KeyCode.Space) && !isHolding) SearchTarget();
        // Release
        if (Input.GetKeyDown(KeyCode.G)) Release();

        // Visual beam
        if (isHolding && lockedObject != null) {
            line.enabled = true;
            line.SetPosition(0, transform.position); // Start at the beam origin
            line.SetPosition(1, lockedObject.transform.position); // End at the trash
        }
        else {
            line.enabled = false;
        }
    }

    void FixedUpdate() {
        if (isHolding && lockedObject != null && !isAttached) {
            float distance = Vector3.Distance(lockedObject.transform.position, holdPoint.position);

            // Increased distance to 0.8f to ensure capture even with large colliders
            if (distance > 0.8f) {
                // PHASE 1: Pulling
                Vector3 direction = holdPoint.position - lockedObject.transform.position;
                lockedRb.AddForce(direction.normalized * pullPower);
                lockedRb.linearVelocity *= 0.9f;
            }
            else {
                // PHASE 2: Hard Attachment
                CaptureHard();
            }
        }
    }

    private void SearchTarget() {
        RaycastHit hit;
        // Start ray from holdPoint to prevent hitting ship's internal colliders
        if (Physics.Raycast(holdPoint.position, transform.forward, out hit, rangeBeam, ~ignoreLayer)) {
            if (hit.collider.CompareTag("Trash")) {
                lockedObject = hit.collider.gameObject;
                lockedRb = lockedObject.GetComponent<Rigidbody>();
                isHolding = true;
                isAttached = false;
            }
        }
    }

    private void CaptureHard() {
        isAttached = true;

        // Save world scale BEFORE parenting to prevent distortion
        Vector3 originalWorldScale = lockedObject.transform.lossyScale;

        // Disable physics
        lockedRb.isKinematic = true;
        lockedRb.linearVelocity = Vector3.zero;
        lockedRb.angularVelocity = Vector3.zero;

        // Disable collisions
        Collider col = lockedObject.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // Parenting
        lockedObject.transform.SetParent(holdPoint);

        // Reset position and rotation relative to holdPoint
        lockedObject.transform.localPosition = Vector3.zero;
        lockedObject.transform.localRotation = Quaternion.identity;

        // We set local scale based on original size vs current parent size
        Vector3 currentParentScale = holdPoint.lossyScale;
        lockedObject.transform.localScale = new Vector3(
            originalWorldScale.x / currentParentScale.x,
            originalWorldScale.y / currentParentScale.y,
            originalWorldScale.z / currentParentScale.z
        );
    }

    private void Release() {
        if (lockedObject != null) {
            lockedObject.transform.SetParent(null);
            lockedRb.isKinematic = false;

            Collider col = lockedObject.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            // Push forward on release
            lockedRb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            lockedObject = null;
        }
        isHolding = false;
        isAttached = false;
    }
}