using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Vector2 originalPositionReset;
    private GameObject[] answerObjects;
    private float distanceThreshold = 180f; // Adjust this value accordingly

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        answerObjects = GameObject.FindGameObjectsWithTag("answer");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPositionReset = rectTransform.anchoredPosition;
        originalPosition = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        answerObjects = GameObject.FindGameObjectsWithTag("answer");
        foreach (GameObject obj in answerObjects)
        {
            if (obj == gameObject)
                continue;

            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance < distanceThreshold)
            {
                SwapPositions(obj);
                return;
            }
        }

        rectTransform.anchoredPosition = originalPositionReset;
    }

    private void SwapPositions(GameObject targetObject)
    {
        Vector2 tempPosition = targetObject.transform.position;
        targetObject.transform.position = originalPosition;
        transform.position = tempPosition;
        string tempName = transform.name;
        transform.name = targetObject.name;
        targetObject.name = tempName;
    }
}
