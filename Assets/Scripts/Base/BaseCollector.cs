using UnityEngine;

public class BaseCollector : MonoBehaviour
{
    private int trashCount = 0;

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Trash")) {
            Destroy(other.gameObject);
            trashCount++;
            Debug.Log("Мусора собрано: " + trashCount);

        }
    }
}
