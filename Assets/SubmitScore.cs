using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SubmitScore : MonoBehaviour
{
    private InputField nameInput;
    [SerializeField]private Text notification;
    private const int MAX = 10;

    private void Awake()
    {
        nameInput = GameObject.Find("NameInput")?.GetComponent<InputField>();
        if (notification != null)
            notification.gameObject.SetActive(false);
    }

    public void OnEnterButton()
    {
        string playerName = string.IsNullOrEmpty(nameInput.text)
            ? "Player"
            : nameInput.text;

        int score = PlayerPrefs.GetInt("FinalScore", 0);

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

        ShowNotification("Points saved successfully!");

        Debug.Log($"? Saved BXH: {playerName} - {score}");
    }
    void ShowNotification(string message)
    {
        if (notification == null) return;

        notification.text = message;
        notification.gameObject.SetActive(true);

        CancelInvoke(nameof(HideNotification));
        Invoke(nameof(HideNotification), 2f);
    }

    void HideNotification()
    {
        if (notification != null) notification.gameObject.SetActive(false);
    }
}
