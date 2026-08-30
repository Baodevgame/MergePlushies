using UnityEngine;

[CreateAssetMenu(menuName = "Ads/Ads Settings")]
public class AdsSettings : ScriptableObject
{
    [Header("Android")]
    public string BannerId;
    public string InterstitialId;
    public string RewardedId;

    [Header("IOS")]
    public string IOSBannerId;
    public string IOSInterstitialId;
    public string IOSRewardedId;
}