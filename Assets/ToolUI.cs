using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button hammerBtn;
    public Button bombBtn;
    public Button magnetBtn;
    public Button swapBtn;

    [Header("Count Text")]
    public Text hammerCountText;
    public Text bombCountText;
    public Text magnetCountText;
    public Text swapCountText;

    private void Start()
    {
        hammerBtn.onClick.AddListener(() => ToolManager.Instance.SelectTool(ToolType.Hammer));
        bombBtn.onClick.AddListener(() => ToolManager.Instance.SelectTool(ToolType.Bomb));
        magnetBtn.onClick.AddListener(() => ToolManager.Instance.SelectTool(ToolType.Magnet));
        swapBtn.onClick.AddListener(() => ToolManager.Instance.SelectTool(ToolType.Swap));

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI(); 
    }

    void UpdateUI()
    {
        int hammer = PlayerPrefs.GetInt("item_Hammer", 0);
        int bomb = PlayerPrefs.GetInt("item_Bomb", 0);
        int magnet = PlayerPrefs.GetInt("item_Magnet", 0);
        int swap = PlayerPrefs.GetInt("item_Swap", 0);

        hammerCountText.text = hammer.ToString();
        bombCountText.text = bomb.ToString();
        magnetCountText.text = magnet.ToString();
        swapCountText.text = swap.ToString();

        hammerBtn.interactable = hammer > 0;
        bombBtn.interactable = bomb > 0;
        magnetBtn.interactable = magnet > 0;
        swapBtn.interactable = swap > 0;
    }
}
