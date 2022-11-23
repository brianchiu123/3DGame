using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragger: MonoBehaviour, IDragHandler
{
    [SerializeField] RectTransform rect;

    [SerializeField] Canvas UICanvas;


    public void OnDrag(PointerEventData eventData)
    {
        rect.transform.localPosition += (Vector3)eventData.delta / UICanvas.scaleFactor;
        // Because the movement delta will be influenced by canvas scale, it should be division by canvas scale.
    }
}