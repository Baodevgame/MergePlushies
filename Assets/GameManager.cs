using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void HomeFromPause()
    {
        AudioManager.Instance.PlayTouch();

        int score = ScoreManager.Instance.GetCurrentScore();
        string randomName = RandomNameGenerator.GetRandomName();

        SaveScoreToLeaderboard.Save(randomName, score);

        PlayerPrefs.DeleteKey("FinalScore"); 

        ScoreManager.Instance.ResetScore();

        Time.timeScale = 1f;
        SceneFader.Instance.LoadScene("MainMenu");
    }

    public void HomeFromGameOver()
    {
        AudioManager.Instance.PlayTouch();

        PlayerPrefs.DeleteKey("FinalScore"); 

        ScoreManager.Instance.ResetScore();

        Time.timeScale = 1f;
        SceneFader.Instance.LoadScene("MainMenu");
    }


    public void PlayGame()
    {
        AudioManager.Instance.PlayTouch();
        Time.timeScale = 1f;
        SceneFader.Instance.LoadScene("Game");
        ScoreManager.Instance?.ResetScore();
    }
    public void RestartGame()
    {
        AudioManager.Instance.PlayTouch();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ScoreManager.Instance.ResetScore();
    }

    public void PauseGame()
    {
        AudioManager.Instance.PlayTouch();
        Time.timeScale = 0f;
        IsPaused = true;
        UIManager.Instance?.ShowPausePanel(true);
    }

    public void ResumeGame()
    {
        AudioManager.Instance.PlayTouch();
        Time.timeScale = 1f;
        IsPaused = false;
        UIManager.Instance?.ShowPausePanel(false);
    }

    public void GameOver()
    {
        int finalScore = ScoreManager.Instance.GetCurrentScore();
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.Save();

        Time.timeScale = 0f;
        UIManager.Instance?.ShowHighScorePanel(true);
    }


}
