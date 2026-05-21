using UnityEngine;
using UnityEngine.EventSystems;

public class ToolCancelByTouch : MonoBehaviour
{
    void Update()
    {
        if (!ToolManager.Instance.isUsingTool)
            return;

#if UNITY_EDITOR
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    void HandleMouse()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        CheckCancel(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void HandleTouch()
    {
        if (Input.touchCount == 0) return;
        Touch t = Input.GetTouch(0);
        if (t.phase != TouchPhase.Began) return;

        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject(t.fingerId))
            return;

        CheckCancel(Camera.main.ScreenToWorldPoint(t.position));
    }

    void CheckCancel(Vector2 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (!hit || hit.collider.GetComponent<IconMerge>() == null)
        {
            ToolManager.Instance.ResetTool();
        }
    }
}
