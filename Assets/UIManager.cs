using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject highScorePanel;

    [Header("Home Buttons")]
    [SerializeField] private Button[] homeButtonsPause;
    [SerializeField] private Button[] homeButtonsGameOver;


    [Header("Buttons")]
    //[SerializeField] private Button[] homeButtons;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        Instance = this;
        SetupButtons();
    }

    private void SetupButtons()
    {
        foreach (Button home in homeButtonsPause)
            home.onClick.AddListener(() => GameManager.Instance.HomeFromPause());

        foreach (Button home in homeButtonsGameOver)
            home.onClick.AddListener(() => GameManager.Instance.HomeFromGameOver());

        restartButton?.onClick.AddListener(() => GameManager.Instance.RestartGame());
        settingButton?.onClick.AddListener(() => GameManager.Instance.PauseGame());
        resumeButton?.onClick.AddListener(() => GameManager.Instance.ResumeGame());
    }

    public void ShowPausePanel(bool show) => pausePanel?.SetActive(show);
    public void ShowHighScorePanel(bool show) => highScorePanel?.SetActive(show);
}
