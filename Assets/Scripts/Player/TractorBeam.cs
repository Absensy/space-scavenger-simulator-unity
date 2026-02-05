using Unity.VisualScripting;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public float pullPower = 200f; //Power pull beam
    public float rangeBeam = 20f; // Rang beam 
    public Transform holdPoint;
    private LineRenderer line;

    void Start() {
        line = GetComponent<LineRenderer>();
        line.enabled = false; //Disabled beam in start game
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rangeBeam)) {
                if (hit.collider.CompareTag("Trash")) {
                    line.enabled = true;
                    line.SetPosition(0, transform.position); //Start line
                    line.SetPosition(1, hit.point);//End line


                    // Pull trash in point ship
                    Vector3 direction = holdPoint.position - hit.collider.transform.position;
                    hit.rigidbody.AddForce(direction.normalized * pullPower);

                }
            }
            else {
                line.enabled = false;
            }
        }
        else {
            line.enabled = false;
        }
    }
}
