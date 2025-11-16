using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    public RectTransform dropZoneRect;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging started: " + gameObject.name);
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (IsOverlappingEnough())
        {
            Debug.Log("Ingerdient dropped inside drop zone with enough overlap!");
            rectTransform.anchoredPosition = originalPosition;
        }
        else
        {
            Debug.Log("Not enough overlap. snapping back ingredient...");
            rectTransform.anchoredPosition = originalPosition;
        }
    }
    private bool IsOverlappingEnough()
    {
        if (dropZoneRect == null)
        {
            Debug.LogWarning("drop zone rect not assigned");
            return false;
        }

        Rect rect1 = GetWorldRect(rectTransform);
        Rect rect2 = GetWorldRect(dropZoneRect);

        Rect overlap = RectIntersection(rect1, rect2);

        float overlapArea = overlap.width * overlap.height;
        float draggedArea = rect1.width * rect1.height;

        float overlapPercent = overlapArea / draggedArea;

        return overlapPercent >= 0.5f;
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        return new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    private Rect RectIntersection(Rect a, Rect b)
    {
        float xMin = Mathf.Max(a.xMin, b.xMin);
        float xMax = Mathf.Min(a.xMax, b.xMax);
        float yMin = Mathf.Max(a.yMin, b.yMin);
        float yMax = Mathf.Min(a.yMax, b.yMax);

        if (xMax >= xMin && yMax >= yMin)
        {
            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }
        else
        {
            return new Rect(0, 0, 0, 0);
        }
    }
}
