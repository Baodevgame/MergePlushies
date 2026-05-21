using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewardTimerUI : MonoBehaviour
{
    public Text countdownText;

    void Update()
    {
        if (DailyRewardManager.Instance == null)
        {
            countdownText.text = "--:--:--";
            return;
        }

        if (DailyRewardManager.Instance.IsTodayAvailable())
        {
            countdownText.text = "Can receive gifts!";
            return;
        }

        TimeSpan t = DailyRewardManager.Instance.TimeUntilNextDay();

        if (t.TotalSeconds <= 0)
        {
            countdownText.text = "Can receive gifts!";
            return;
        }

        countdownText.text =
            string.Format("{0:D2}:{1:D2}:{2:D2}",t.Hours,t.Minutes,t.Seconds);
    }
}
