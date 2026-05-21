using UnityEngine;
using System;

public class LoginTracker : MonoBehaviour
{
    void Start()
    {
        string today = DateTime.Now.ToString("yyyyMMdd");
        string last = PlayerPrefs.GetString("LAST_LOGIN", "");

        if (today != last)
        {
            PlayerPrefs.SetString("LAST_LOGIN", today);
            PlayerData.Add(AchievementType.LoginDays);
        }
    }
}
