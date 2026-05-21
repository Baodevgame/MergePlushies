using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private UIFader scoreAchievementPanel;
    [SerializeField] private UIFader loginAchievementPanel;
    [SerializeField] private UIFader toolAchievementPanel;

    private UIFader currentPanel;

    private void Start()
    {
        currentPanel = scoreAchievementPanel;

        scoreAchievementPanel.FadeIn();

        loginAchievementPanel.gameObject.SetActive(false);
        toolAchievementPanel.gameObject.SetActive(false);
    }

    public void OnScorePanel()
    {
        AudioManager.Instance.PlayTouch();

        SwitchPanel(scoreAchievementPanel);
    }

    public void OnLoginPanel()
    {
        AudioManager.Instance.PlayTouch();

        SwitchPanel(loginAchievementPanel);
    }

    public void OnToolPanel()
    {
        AudioManager.Instance.PlayTouch();

        SwitchPanel(toolAchievementPanel);
    }

    void SwitchPanel(UIFader newPanel)
    {
        if (currentPanel == newPanel)
            return;

        currentPanel.FadeOut();

        currentPanel = newPanel;

        currentPanel.FadeIn();
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.PlayTouch();

        SceneFader.Instance.LoadScene("MainMenu");
    }
}