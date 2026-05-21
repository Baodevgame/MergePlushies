using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private Text scoreText;
    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        UpdateScore();
    }

    private void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        if (ScoreManager.Instance != null)scoreText.text = "HighScore : " + ScoreManager.Instance.GetCurrentScore();
    }
}
