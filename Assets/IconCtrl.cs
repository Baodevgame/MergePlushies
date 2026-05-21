using UnityEngine;
using UnityEngine.EventSystems;
public class IconCtrl : MonoBehaviour
{
    private IconMerge merge;
    public System.Action onDropCallback;
    private Rigidbody2D rb;
    public bool isReleased = false;
    private bool isDragging = false;
    private float zPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        merge = GetComponent<IconMerge>();
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }
    private void Start()
    {
        zPos = Camera.main.WorldToScreenPoint(transform.position).z;
    }
    private void Update()
    {
        if (ToolManager.Instance.isUsingTool || ToolManager.Instance.blockInput) return;
        if (isReleased) return;
#if UNITY_EDITOR
        HandleMouseInput();
#else
HandleTouchInput(); 
#endif 
    } // ===================== TOUCH =========================== 
private void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;
        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began && !IsPointerOverUI(t))
        {
            isDragging = true;
            SnapToPointer(t.position);
        }
        if ((t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary) && isDragging)
        {
            MoveWithPointer(t.position);
        }
        if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
        {
            if (isDragging) DropIcon(t.position);
            isDragging = false;
        }
    }
    // ===================== MOUSE =========================== 
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            isDragging = true;
            SnapToPointer(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            MoveWithPointer(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging) DropIcon(Input.mousePosition);
            isDragging = false;
        }
    }
    // ===================== MOVING =========================== 
    private void SnapToPointer(Vector2 pointer)
    {
        Vector3 pos = new Vector3(pointer.x, pointer.y, zPos);
        Vector3 world = Camera.main.ScreenToWorldPoint(pos);
        rb.position = new Vector2(world.x, rb.position.y);
    }
    private void MoveWithPointer(Vector2 pointer)
    {
        Vector3 pos = new Vector3(pointer.x, pointer.y, zPos);
        Vector3 world = Camera.main.ScreenToWorldPoint(pos);
        rb.position = new Vector2(world.x, rb.position.y);
    }
    // ===================== DROP =========================== 
    private void DropIcon(Vector2 pointer)
    {
        MoveWithPointer(pointer);
        rb.gravityScale = 1f;
        isReleased = true;
        if (merge != null)
        {
            merge.isPlaced = true; 
        }
        onDropCallback?.Invoke();
        Destroy(this);
    }
    // ===================== UI CHECK =========================== 
    private bool IsPointerOverUI(Touch t)
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(t.fingerId);
    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}