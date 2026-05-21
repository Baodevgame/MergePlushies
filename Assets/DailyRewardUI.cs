using UnityEngine;
using UnityEngine.UI;

public class DailyRewardUI : MonoBehaviour
{
    [Header("Tick Images")]
    public GameObject[] grayTicks;   
    public GameObject[] greenTicks;  

    [Header("Button")]
    public Button claimButton;

    private void Start()
    {
        claimButton.onClick.AddListener(OnClaimClick);
        RefreshUI();
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    void OnClaimClick()
    {
        AudioManager.Instance.PlayTouch();
        DailyRewardManager.Instance.ClaimReward();
        RefreshUI();
    }

    void RefreshUI()
    {
        if (DailyRewardManager.Instance == null) return;

        for (int i = 0; i < 7; i++)
        {
            int day = i + 1;

            bool claimed = DailyRewardManager.Instance.IsDayClaimed(day);

            grayTicks[i].SetActive(!claimed);
            greenTicks[i].SetActive(claimed);
        }

        claimButton.interactable =DailyRewardManager.Instance.CanClaimToday();
    }

}
