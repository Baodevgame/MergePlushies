using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsUI : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        RewardedAdManager.Instance.ShowReward(() =>
        {
            CurrencyManager.Instance.AddGold(100);
        });
    }
}
