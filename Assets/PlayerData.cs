using UnityEngine;

public static class PlayerData
{
    public static int Get(AchievementType type)
    {
        return PlayerPrefs.GetInt(type.ToString(), 0);
    }
    public static void Add(AchievementType type, int value = 1)
    {
        PlayerPrefs.SetInt(type.ToString(), Get(type) + value);
    }

    public static void SetMax(AchievementType type, int value)
    {
        int old = Get(type);
        if (value > old)
            PlayerPrefs.SetInt(type.ToString(), value);
    }

    public static bool IsClaimed(AchievementType type, int target)
    {
        return PlayerPrefs.GetInt($"{type}_{target}_CLAIM", 0) == 1;
    }

    public static void SetClaimed(AchievementType type, int target)
    {
        PlayerPrefs.SetInt($"{type}_{target}_CLAIM", 1);
    }
}
