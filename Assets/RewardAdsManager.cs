using UnityEngine;
using GoogleMobileAds.Api;

public class RewardedAdManager : MonoBehaviour
{
    public static RewardedAdManager Instance;

    private RewardedAd rewardedAd;

    private string adUnitId = "ca-app-pub-2359977462573228/4025600407";
    //private string adUnitId = "ca-app-pub-3940256099942544/5224354917";



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
            return;
        }

    }
    private void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("? Failed to load rewarded ad");
                    return;
                }

                rewardedAd = ad;
                Debug.Log("? Rewarded ad loaded");
            });
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show(reward =>
            {
                CurrencyManager.Instance.AddGold(100);
                LoadRewardedAd();
            });
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }
}
