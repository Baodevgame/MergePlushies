using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class ScoreData
    {
        public string name;
        public int score;
    }

    [Header("UI")]
    public Text[] nameTexts;   
    public Text[] scoreTexts;  

    private const int MAX = 10;
    private List<ScoreData> scores = new List<ScoreData>();

    private void Start()
    {
        LoadFromPrefs();

        RefreshUI();
    }

    void RefreshUI()
    {
        for (int i = 0; i < MAX; i++)
        {
            if (i < scores.Count)
            {
                nameTexts[i].text = scores[i].name;
                scoreTexts[i].text = scores[i].score.ToString();
            }
            else
            {
                nameTexts[i].text = "---";
                scoreTexts[i].text = "0";
            }
        }
    }

    void LoadFromPrefs()
    {
        scores.Clear();
        for (int i = 0; i < MAX; i++)
        {
            if (PlayerPrefs.HasKey($"Score_{i}"))
            {
                scores.Add(new ScoreData{name = PlayerPrefs.GetString($"Name_{i}"),score = PlayerPrefs.GetInt($"Score_{i}")});
            }
        }
    }
    public void BackToMainMenu()
    {
        SceneFader.Instance.LoadScene("MainMenu");
        AudioManager.Instance.PlayTouch();
    }
}
