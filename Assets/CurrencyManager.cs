using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private int gold;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gold = PlayerPrefs.GetInt("gold",500) ; 
        //PlayerPrefs.DeleteAll(); 
    }

    public int GetGold()
    {
        return gold;
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;

        gold -= amount;
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
        return true;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
    }
}
