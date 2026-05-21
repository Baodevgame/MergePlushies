using UnityEngine;
using System.Collections;
public enum ToolType
{
    None,
    Hammer,
    Bomb,
    Magnet,
    Swap
}
public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;
    public ToolType currentTool = ToolType.None;
    public bool isUsingTool = false; // dang trong mode dung tool 
    public bool blockInput = false; // chan icon roi sau khi dung tool 
    private IconMerge firstSelected;

    [SerializeField]
    private GameObject bombFXPrefab;
    private void Awake() { Instance = this; }
    // Chon Tool 
    public void SelectTool(ToolType tool)
    {
        // khong cho chon neu khong co item 
        if (!HasToolItem(tool))
        {
            Debug.Log("No item for tool: " + tool);
            ResetTool(); return;
        }
        AudioManager.Instance.PlayTouch();
        currentTool = tool;
        isUsingTool = true;
        firstSelected = null;
        Debug.Log("Selected tool: " + tool);
    }
    bool HasToolItem(ToolType tool)
    {
        switch (tool)
        {
            case ToolType.Hammer: return ItemShop.Instance.GetItemCount(ItemType.Hammer) > 0;
            case ToolType.Bomb: return ItemShop.Instance.GetItemCount(ItemType.Bomb) > 0;
            case ToolType.Magnet: return ItemShop.Instance.GetItemCount(ItemType.Magnet) > 0;
            case ToolType.Swap: return ItemShop.Instance.GetItemCount(ItemType.Swap) > 0;
        }
        return false;
    }
    // Click vao icon khi dang dung tool 
    public void OnIconClicked(IconMerge icon)
    {
        if (icon == null) return;
        switch (currentTool)
        {
            case ToolType.Hammer:
                UseHammer(icon);
                break;
            case ToolType.Bomb:
                UseBomb(icon.transform.position);
                break;
            case ToolType.Magnet:
                HandleTwoSelect(icon, UseMagnet);
                break;
            case ToolType.Swap:
                HandleTwoSelect(icon, UseSwap);
                break;
        }
    }
    // ------------------------- // Tool: HAMMER // ------------------------- 
    private void UseHammer(IconMerge target)
    {
        if (!ItemShop.Instance.UseItem(ItemType.Hammer))
        {
            ResetTool();
            return;
        }
        PlayerData.Add(AchievementType.UseHammer);
        CheckComboTool();
        CheckTotalTool();
        target.gameObject.SetActive(false);
        AudioManager.Instance.PlayHammer();
        ResetTool();
    }
    // ------------------------- // Tool: BOMB // ------------------------- 
    private void UseBomb(Vector3 center)
    {
        if (!ItemShop.Instance.UseItem(ItemType.Bomb))
        {
            ResetTool();
            return;
        }

        Instantiate(bombFXPrefab,center,Quaternion.identity);

        float radius = 0.5f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius);
        foreach (var c in hits)
        {
            IconMerge icon = c.GetComponent<IconMerge>();
            if (icon != null && icon.gameObject.activeSelf)
            {
                icon.gameObject.SetActive(false);
            }
        }
        PlayerData.Add(AchievementType.UseBomb);
        CheckComboTool();
        CheckTotalTool();
        AudioManager.Instance.PlayBomb();
        ResetTool();
    }
    // ------------------------- // Tool: MAGNET // ------------------------- 
    private void UseMagnet(IconMerge a, IconMerge b)
    {
        if (a == b || a.level != b.level)
        {
            Debug.Log("? Magnet fail: level not match");
            ResetTool();
            return;
        }
        if (!ItemShop.Instance.UseItem(ItemType.Magnet))
        {
            ResetTool();
            return;
        }
        a.MergeWith(b); 
        PlayerData.Add(AchievementType.UseMagnet);
        CheckComboTool();
        CheckTotalTool();
        AudioManager.Instance.PlayMagnet();
        ResetTool();
    }
    // ------------------------- // Tool: SWAP // ------------------------- 
    private void UseSwap(IconMerge a, IconMerge b)
    {
        if (!ItemShop.Instance.UseItem(ItemType.Swap))
        {
            ResetTool();
            return;
        }
        Vector3 temp = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = temp;
        Rigidbody2D ra = a.GetComponent<Rigidbody2D>();
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        if (ra) ra.velocity = Vector2.zero;
        if (rb) rb.velocity = Vector2.zero;
        PlayerData.Add(AchievementType.UseSwap);
        CheckComboTool();
        CheckTotalTool();
        AudioManager.Instance.PlaySwap();
        ResetTool();
    }
    // Tool can chon 2 icon (Magnet, Swap) 
    private void HandleTwoSelect(IconMerge clicked, System.Action<IconMerge, IconMerge> action)
    {
        if (firstSelected == null)
        {
            firstSelected = clicked;
        }
        else
        {
            action.Invoke(firstSelected, clicked);
            firstSelected = null;
        }
    }
    // ------------------------- // RESET TOOL // ------------------------- 
    public void ResetTool()
    {
        currentTool = ToolType.None;
        isUsingTool = false;
        StartCoroutine(BlockInputForFrames(2)); // chan input 2 frame 
        firstSelected = null;
        Debug.Log("Tool reset");
    }
    void CheckComboTool()
    {
        int hammer = PlayerData.Get(AchievementType.UseHammer);
        int magnet = PlayerData.Get(AchievementType.UseMagnet);
        PlayerData.SetMax(AchievementType.UseHammerAndMagnet, Mathf.Min(hammer, magnet));
        int bomb = PlayerData.Get(AchievementType.UseBomb);
        int swap = PlayerData.Get(AchievementType.UseSwap);
        PlayerData.SetMax(AchievementType.UseBombAndSwap, Mathf.Min(bomb, swap));
        Debug.Log($"[Combo] HM={Mathf.Min(hammer, magnet)} BS={Mathf.Min(bomb, swap)}");
    }
    void CheckTotalTool()
    {
        int total = PlayerData.Get(AchievementType.UseHammer) + PlayerData.Get(AchievementType.UseBomb) + PlayerData.Get(AchievementType.UseMagnet) + PlayerData.Get(AchievementType.UseSwap);
        PlayerData.SetMax(AchievementType.UseTotalTool, total);
        Debug.Log("Total tool used = " + total);
    }
    // Chan icon roi ngay sau khi dung tool 
    private IEnumerator BlockInputForFrames(int frames)
    {
        blockInput = true;
        for (int i = 0; i < frames; i++)
            yield return null;
        blockInput = false;
    }
}