using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardedAdManager : MonoBehaviour
{
    public static RewardedAdManager Instance;

    [SerializeField]
    private AdsSettings adsSettings;

    private RewardedAd rewardedAd;

    private float retryDelay = 5f;

    private const float MAX_RETRY = 300f;

    private Action rewardCallback;

    private string RewardId
    {
        get
        {
#if UNITY_ANDROID
            return adsSettings.RewardedId;
#elif UNITY_IOS
            return adsSettings.IOSRewardedId;
#else
            return "unused";
#endif
        }
    }

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
    }

    private void Start()
    {
        InvokeRepeating(nameof(CheckInit), 0f, 1f);
    }

    private void CheckInit()
    {
        if (!AdsInitializer.IsInitialized)
            return;

        CancelInvoke(nameof(CheckInit));

        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        AdRequest request = new AdRequest();

        RewardedAd.Load(RewardId,request,(RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Reward Load Failed: " + error);

                    Invoke(nameof(LoadRewardedAd),retryDelay);

                    retryDelay =Mathf.Min(retryDelay * 2f,MAX_RETRY);

                    return;
                }

                rewardedAd?.Destroy();

                rewardedAd = ad;

                retryDelay = 5f;

                RegisterEvents(ad);

                Debug.Log("Reward Loaded");
            });
    }

    private void RegisterEvents(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            AdsAudioUtility.Resume();

            rewardedAd?.Destroy();
            rewardedAd = null;

            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                AdsAudioUtility.Resume();

                rewardedAd?.Destroy();
                rewardedAd = null;

                LoadRewardedAd();
            };
    }

    public bool IsReady()
    {
        return rewardedAd != null && rewardedAd.CanShowAd();
    }

    public void ShowReward(Action onReward)
    {
        Debug.Log("rewardedAd = " + (rewardedAd == null ? "NULL" : "EXISTS"));

        if (rewardedAd != null)
        {
            Debug.Log("CanShowAd = " + rewardedAd.CanShowAd());
        }

        if (!IsReady())
        {
            Debug.Log("Reward not ready");
            return;
        }

        rewardCallback = onReward;

        AdsAudioUtility.Pause();

        rewardedAd.Show(reward =>
        {
            rewardCallback?.Invoke();
            rewardCallback = null;
        });
    }

    private void OnDestroy()
    {
        rewardedAd?.Destroy();
    }
}