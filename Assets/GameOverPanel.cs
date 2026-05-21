using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text yourScoreText;

    private void OnEnable()
    {
        if (ScoreManager.Instance != null)
        {
            yourScoreText.text ="Your Score: " + ScoreManager.Instance.GetCurrentScore();
        }
    }
}
