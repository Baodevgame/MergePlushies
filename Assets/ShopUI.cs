using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Text goldText;

    public Text hammerCount;
    public Text magnetCount;
    public Text bombCount;
    public Text swapCount;

    public Text hammerPrice;
    public Text magnetPrice;
    public Text bombPrice;
    public Text swapPrice;

    public Button hammerBtn;
    public Button magnetBtn;
    public Button bombBtn;
    public Button swapBtn;
    public Button backBtn;

    void Start()
    {
        hammerPrice.text = ItemShop.Instance.GetPrice(ItemType.Hammer) + " G";
        magnetPrice.text = ItemShop.Instance.GetPrice(ItemType.Magnet) + " G";
        bombPrice.text = ItemShop.Instance.GetPrice(ItemType.Bomb) + " G";
        swapPrice.text = ItemShop.Instance.GetPrice(ItemType.Swap) + " G";
        backBtn.onClick.AddListener(OnBackClick);
        BannerAdManager.Instance.ShowBanner();
    }

    void Update()
    {
        goldText.text = CurrencyManager.Instance.GetGold().ToString() + " G";

        hammerCount.text = "Count: " + ItemShop.Instance.GetItemCount(ItemType.Hammer).ToString();
        magnetCount.text = "Count: " + ItemShop.Instance.GetItemCount(ItemType.Magnet).ToString();
        bombCount.text = "Count: " + ItemShop.Instance.GetItemCount(ItemType.Bomb).ToString();
        swapCount.text = "Count: " + ItemShop.Instance.GetItemCount(ItemType.Swap).ToString();

        hammerBtn.interactable = CurrencyManager.Instance.GetGold() >= ItemShop.Instance.GetPrice(ItemType.Hammer);
        magnetBtn.interactable = CurrencyManager.Instance.GetGold() >= ItemShop.Instance.GetPrice(ItemType.Magnet);
        bombBtn.interactable = CurrencyManager.Instance.GetGold() >= ItemShop.Instance.GetPrice(ItemType.Bomb);
        swapBtn.interactable = CurrencyManager.Instance.GetGold() >= ItemShop.Instance.GetPrice(ItemType.Swap);
    }

    void OnBackClick()
    {
        AudioManager.Instance.PlayTouch();
        BannerAdManager.Instance.HideBanner();
        SceneFader.Instance.LoadScene("MainMenu");

    }
}
