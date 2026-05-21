using UnityEngine;
using System;
using System.Globalization;

public class DailyRewardManager : MonoBehaviour
{
    public static DailyRewardManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // =======================
    // TIME
    // =======================
    int GetWeek()
    {
        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            GameTime.Now,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday
        );
    }

    int GetDay()
    {
        int d = (int)GameTime.Now.DayOfWeek;
        return d == 0 ? 7 : d; 
    }

    public int CurrentWeek => GetWeek();
    public int CurrentDay => GetDay();

    // =======================
    // CHECK CLAIM
    // =======================
    public bool CanClaimToday()
    {
        int savedWeek = PlayerPrefs.GetInt("daily_week", -1);
        int savedDay = PlayerPrefs.GetInt("daily_day", -1);

        return savedWeek != CurrentWeek || savedDay != CurrentDay;
    }

    public bool IsDayClaimed(int day)
    {
        int savedWeek = PlayerPrefs.GetInt("daily_week", -1);
        int savedDay = PlayerPrefs.GetInt("daily_day", -1);

        if (savedWeek != CurrentWeek)
            return false;

        return savedDay == day;
    }


    // =======================
    // CLAIM
    // =======================
    public void ClaimReward()
    {
        if (!CanClaimToday()) return;

        int today = CurrentDay;

        GiveReward(today);

        PlayerPrefs.SetInt("daily_week", CurrentWeek);
        PlayerPrefs.SetInt("daily_day", today);
        PlayerPrefs.Save();
    }

    // =======================
    // REWARD
    // =======================
    void GiveReward(int day)
    {
        switch (day)
        {
            case 1: CurrencyManager.Instance.AddGold(50); break;
            case 2: CurrencyManager.Instance.AddGold(80); break;
            case 3: CurrencyManager.Instance.AddGold(120); break;
            case 4: CurrencyManager.Instance.AddGold(180); break;
            case 5: AddItem(ItemType.Hammer, 1); AddItem(ItemType.Magnet, 1); AddItem(ItemType.Bomb, 1); AddItem(ItemType.Swap, 1); break;
            case 6: CurrencyManager.Instance.AddGold(350); break;
            case 7: CurrencyManager.Instance.AddGold(500); break;
        }
    }

    void AddItem(ItemType type, int amount)
    {
        int count = PlayerPrefs.GetInt("item_" + type, 0);
        PlayerPrefs.SetInt("item_" + type, count + amount);
        PlayerPrefs.Save();
    }

    public TimeSpan TimeUntilNextDay()
    {
        DateTime now = GameTime.Now;

        DateTime nextDay = new DateTime(now.Year,now.Month,now.Day,0, 0, 0).AddDays(1);

        return nextDay - now;
    }

    public bool IsTodayAvailable()
    {
        return CanClaimToday();
    }

}
