using UnityEngine;

public static class RandomNameGenerator
{
    static string[] names =
    {
        "Guest",
        "Player",
        "Hero",
        "NoName"
    };

    public static string GetRandomName()
    {
        int number = Random.Range(1000, 9999);
        string baseName = names[Random.Range(0, names.Length)];
        return baseName + "_" + number;
    }
}
