using UnityEngine;
using System.Collections.Generic;

public static class SaveScoreToLeaderboard
{
    const int MAX = 10;

    public static void Save(string playerName, int score)
    {
        List<(string name, int score)> list = new();

        for (int i = 0; i < MAX; i++)
        {
            if (PlayerPrefs.HasKey($"Score_{i}"))
            {
                list.Add((
                    PlayerPrefs.GetString($"Name_{i}"),
                    PlayerPrefs.GetInt($"Score_{i}")
                ));
            }
        }

        list.Add((playerName, score));
        list.Sort((a, b) => b.score.CompareTo(a.score));

        if (list.Count > MAX)
            list.RemoveRange(MAX, list.Count - MAX);

        for (int i = 0; i < list.Count; i++)
        {
            PlayerPrefs.SetString($"Name_{i}", list[i].name);
            PlayerPrefs.SetInt($"Score_{i}", list[i].score);
        }

        PlayerPrefs.Save();
    }
}
