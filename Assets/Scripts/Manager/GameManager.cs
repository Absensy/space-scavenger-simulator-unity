using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance; //Global access to the manager
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        UpdateScoreUI();
    }


    public void AddScore(int amount) {
        score += amount;
        UpdateScoreUI();
    }
    public void UpdateScoreUI() {
        if (scoreText != null) {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    

}
