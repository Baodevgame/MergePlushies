using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject dailyGiftPanel;

    private void Start()
    {

        playButton.onClick.AddListener(() => GameManager.Instance.PlayGame());
        dailyGiftPanel.SetActive(false);
    }
    public void Shop()
    {
        AudioManager.Instance.PlayTouch();
        SceneFader.Instance.LoadScene("Shop");
    }
    public void HighScore()
    {
        AudioManager.Instance.PlayTouch();
        SceneFader.Instance.LoadScene("HighScore");
    }
    public void DailyRewardGift()
    {
        AudioManager.Instance.PlayTouch();
        dailyGiftPanel.SetActive(true);
    }
    public void Achievement()
    {
        AudioManager.Instance.PlayTouch();
        SceneFader.Instance.LoadScene("Achievement");
    }
    public void Close()
    {
        AudioManager.Instance.PlayTouch();
        dailyGiftPanel.SetActive(false);
    }
}
