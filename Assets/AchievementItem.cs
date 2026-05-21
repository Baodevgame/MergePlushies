using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{
    [Header("Achievement")]
    public AchievementType type;
    public int target;

    [Header("Description")]
    [TextArea]
    public string descriptionTemplate;

    [Header("Reward")]
    public RewardType rewardType;
    public int rewardAmount;

    [Header("UI")]
    public Button claimButton;
    public Text descriptionText;

    void OnEnable()
    {
        Refresh();
    }

    void Refresh()
    {
        int current = PlayerData.Get(type);
        int display = Mathf.Min(current, target);
        bool claimed = PlayerData.IsClaimed(type, target);

        descriptionText.text = string.Format(descriptionTemplate,display.ToString("N0"),target.ToString("N0"));
        claimButton.interactable = current >= target && !claimed;
    }

    public void Claim()
    {
        if (PlayerData.IsClaimed(type, target))
            return;

        PlayerData.SetClaimed(type, target);
        GiveReward();
        Refresh();
    }

    void GiveReward()
    {
        switch (rewardType)
        {
            case RewardType.Gold:
                CurrencyManager.Instance.AddGold(rewardAmount);
                Debug.Log("Gold after claim = " + CurrencyManager.Instance.GetGold());
                break;

            case RewardType.Hammer:
                ItemShop.Instance.AddItem(ItemType.Hammer, rewardAmount);
                break;

            case RewardType.Bomb:
                ItemShop.Instance.AddItem(ItemType.Bomb, rewardAmount);
                break;

            case RewardType.Magnet:
                ItemShop.Instance.AddItem(ItemType.Magnet, rewardAmount);
                break;

            case RewardType.Swap:
                ItemShop.Instance.AddItem(ItemType.Swap, rewardAmount);
                break;
        }
    }
}
