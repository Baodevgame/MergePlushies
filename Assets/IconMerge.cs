using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IconMerge : MonoBehaviour
{
    [Header("Information")] public int level = 1;
    public GameObject nextLevelPrefab;
    [Header("Status")]
    public bool isMerging = false; 
    public bool isPlaced = false; 
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void MergeWith(IconMerge other)
    {
        // Ngan goi merge trung 
        if (isMerging || other.isMerging) return;
        // Kiem tra cung level 
        if (level == other.level)
        {
            isMerging = true;
            other.isMerging = true;
            Transform lower = transform.position.y < other.transform.position.y ? transform : other.transform;
            Vector3 spawnPos = lower.position;
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            if (nextLevelPrefab != null)
            {
                GameObject merged = MyPoolManager.Instance.Get(nextLevelPrefab, spawnPos);
                var newMerge = merged.GetComponent<IconMerge>();
                if (newMerge != null)
                {
                    newMerge.isMerging = false;
                    newMerge.isPlaced = true;
                }
                Rigidbody2D rbNew = merged.GetComponent<Rigidbody2D>();
                if (rbNew != null)
                {
                    rbNew.velocity = Vector2.zero;
                    rbNew.angularVelocity = 0f;
                    rbNew.gravityScale = 1;
                }
                AudioManager.Instance.PlayMerge();
            }
            // tinh diem 
            if (ScoreManager.Instance != null) ScoreManager.Instance.AddScore(level * 10);
            Debug.Log($"Merge thanh icon level {level + 1}");
        }
    }
    private void OnMouseDown()
    {
        if (!isPlaced) return;
        if (ToolManager.Instance != null && ToolManager.Instance.currentTool != ToolType.None)
        {
            ToolManager.Instance.OnIconClicked(this);
            Debug.Log("Click icon level " + level);
        }
        Debug.Log("Click icon level " + level);
    }
}