using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public void BuyHammer()
    {
        AudioManager.Instance.PlayTouch(); ;
        ItemShop.Instance.BuyItem(ItemType.Hammer);
    }

    public void BuyMagnet()
    {
        AudioManager.Instance.PlayTouch();
        ItemShop.Instance.BuyItem(ItemType.Magnet);
    }

    public void BuyBomb()
    {
        AudioManager.Instance.PlayTouch();
        ItemShop.Instance.BuyItem(ItemType.Bomb);
    }

    public void BuySwap()
    {
        AudioManager.Instance.PlayTouch();
        ItemShop.Instance.BuyItem(ItemType.Swap);
    }
}
