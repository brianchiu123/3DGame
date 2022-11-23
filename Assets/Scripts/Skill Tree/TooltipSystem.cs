using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    static TooltipSystem tooltipSystem;

    [SerializeField] Tooltip tooltip;

    [SerializeField] Camera canvasCamera;

    [SerializeField] float offsetRadius;

    void Awake()
    {
        tooltipSystem = this;
    }

    public static void Show(string header, string desc)
    {
        tooltipSystem.tooltip.SetText(header, desc);
        tooltipSystem.tooltip.gameObject.SetActive(true);

        Vector2 position = Input.mousePosition;
        var pivot = new Vector2(position.x / Screen.width, position.y / Screen.height);
        var offset = (pivot - (Vector2.one / 2)).normalized * tooltipSystem.offsetRadius;
        tooltipSystem.tooltip.GetComponent<RectTransform>().pivot = pivot + offset;
        
        tooltipSystem.tooltip.transform.position = tooltipSystem.canvasCamera.ScreenToWorldPoint(position);
    }

    public static void Hide()
    {
        tooltipSystem.tooltip.gameObject.SetActive(false);
    }
}
