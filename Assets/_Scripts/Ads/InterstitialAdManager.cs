using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterstitialAdManager : MonoBehaviour
{
    public static InterstitialAdManager Instance;

    [SerializeField]
    private AdsSettings adsSettings;

    private InterstitialAd interstitialAd;

    private Action closeCallback;

    private float retryDelay = 5f;

    private const float MAX_RETRY = 300f;

    private string InterId
    {
        get
        {
#if UNITY_ANDROID
            return adsSettings.InterstitialId;
#elif UNITY_IOS
            return adsSettings.IOSInterstitialId;
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
        InvokeRepeating(nameof(CheckInit), 0, 1);
    }

    private void CheckInit()
    {
        if (!AdsInitializer.IsInitialized)
            return;

        CancelInvoke(nameof(CheckInit));

        LoadInterstitial();
    }

    public void LoadInterstitial()
    {
        AdRequest request = new AdRequest();

        InterstitialAd.Load(InterId,request,(InterstitialAd ad,LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Invoke( nameof(LoadInterstitial),retryDelay);

                    retryDelay = Mathf.Min(retryDelay * 2,MAX_RETRY);

                    return;
                }

                interstitialAd?.Destroy();

                interstitialAd = ad;

                retryDelay = 5f;

                RegisterEvents(ad);
            });
    }

    private void RegisterEvents(
        InterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            AdsAudioUtility.Resume();

            closeCallback?.Invoke();
            closeCallback = null;

            LoadInterstitial();
        };
    }

    public bool IsReady()
    {
        return interstitialAd != null && interstitialAd.CanShowAd();
    }

    public void ShowInterstitial(
        Action onClosed = null)
    {
        if (!IsReady())
        {
            onClosed?.Invoke();
            return;
        }

        closeCallback = onClosed;

        AdsAudioUtility.Pause();

        interstitialAd.Show();
    }

    private void OnDestroy()
    {
        interstitialAd?.Destroy();
    }
}