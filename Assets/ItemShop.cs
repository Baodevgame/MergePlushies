using UnityEngine;
using System.Collections.Generic;

public class ItemShop : MonoBehaviour
{
    public static ItemShop Instance;

    private Dictionary<ItemType, int> itemPrices = new Dictionary<ItemType, int>()
    {
        { ItemType.Hammer, 100 },
        { ItemType.Magnet, 200 },
        { ItemType.Bomb,   250 },
        { ItemType.Swap,    500 }
    };

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    string Key(ItemType type)
    {
        return "item_" + type.ToString();
    }
    public bool BuyItem(ItemType type)
    {
        int price = itemPrices[type];

        if (!CurrencyManager.Instance.SpendGold(price))
        {
            Debug.Log("Not enough gold!");
            return false;
        }

        int count = PlayerPrefs.GetInt(Key(type), 0);
        PlayerPrefs.SetInt(Key(type), count + 1);
        PlayerPrefs.Save();

        Debug.Log($"Buy {type} | Donate: {count + 1}");
        return true;
    }

    public int GetItemCount(ItemType type)
    {
        return PlayerPrefs.GetInt(Key(type), 0);
    }

    public bool UseItem(ItemType type)
    {
        int count = GetItemCount(type);
        if (count <= 0) return false;

        PlayerPrefs.SetInt(Key(type), count - 1);
        PlayerPrefs.Save();
        return true;
    }
    public int GetPrice(ItemType type)
    {
        return itemPrices[type];
    }

    public void AddItem(ItemType type, int amount)
    {
        int count = PlayerPrefs.GetInt(Key(type), 0);
        PlayerPrefs.SetInt(Key(type), count + amount);
        PlayerPrefs.Save();

        Debug.Log($"AddItem: {type} +{amount} (Total: {count + amount})");
    }
}
