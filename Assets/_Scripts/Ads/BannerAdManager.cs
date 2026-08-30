using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdManager : MonoBehaviour
{
    public static BannerAdManager Instance;

    [SerializeField]
    private AdsSettings adsSettings;

    private BannerView bannerView;

    private float retryDelay = 5f;
    private const float MAX_RETRY_DELAY = 300f;

    private string BannerId
    {
        get
        {
#if UNITY_ANDROID
            return adsSettings.BannerId;
#elif UNITY_IOS
            return adsSettings.IOSBannerId;
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
        InvokeRepeating(nameof(CheckInitialization), 0f, 1f);
    }

    private void CheckInitialization()
    {
        if (!AdsInitializer.IsInitialized)
            return;

        CancelInvoke(nameof(CheckInitialization));

        LoadBanner();
    }

    public void LoadBanner()
    {
        DestroyBanner();

        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        //AdSize adSize = AdSize.Banner;

        bannerView = new BannerView(BannerId,adSize,AdPosition.Bottom);

        RegisterEvents();

        AdRequest request = new AdRequest();

        bannerView.LoadAd(request);
    }

    private void RegisterEvents()
    {
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner Loaded");

            retryDelay = 5f;

            bannerView.Show();
        };

        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError($"Banner Load Failed : {error}");

            Invoke(nameof(LoadBanner), retryDelay);

            retryDelay = Mathf.Min(retryDelay * 2f,MAX_RETRY_DELAY);
        };

        bannerView.OnAdPaid += (AdValue value) =>
        {
            Debug.Log($"Banner Revenue : {value.Value} {value.CurrencyCode}");
        };
    }

    public void ShowBanner()
    {
        Debug.Log("ShowBanner Called");

        if (bannerView == null)
        {
            Debug.Log("Banner NULL");
            LoadBanner();
            return;
        }

        Debug.Log("Banner Show");

        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView?.Hide();
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }

    private void OnDestroy()
    {
        DestroyBanner();
    }
}