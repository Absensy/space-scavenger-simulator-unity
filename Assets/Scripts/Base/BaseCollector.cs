using UnityEngine;

public class BaseCollector : MonoBehaviour
{
    private int pointsPerTrash = 10; //Reward for each item

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Trash")) {
            if(GameManager.instance != null) {
                GameManager.instance.AddScore(pointsPerTrash);
            }
            Destroy(other.gameObject);
        }
    }
}
