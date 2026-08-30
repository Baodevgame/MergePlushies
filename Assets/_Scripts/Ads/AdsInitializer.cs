using UnityEngine;
using GoogleMobileAds.Api;

public class AdsInitializer : MonoBehaviour
{
    public static AdsInitializer Instance;

    public static bool IsInitialized;

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

        if (IsInitialized)
            return;

        MobileAds.Initialize(initStatus =>
        {
            IsInitialized = true;

            Debug.Log("AdMob Initialized");
        });
    }
}