using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    static PathDrawer pathDrawer;

    LineRenderer lineRenderer;

    void Awake()
    {
        pathDrawer = this;

        lineRenderer = GetComponent<LineRenderer>();
    }

    public static void DrawPath(GameObject gameObject, Vector3 fromTo)
    {
        LineRenderer path = gameObject.AddComponent<LineRenderer>();

        LineRenderer template = pathDrawer.lineRenderer;

        path.material = template.material;
        path.widthMultiplier = template.widthMultiplier;
        path.positionCount = template.positionCount;
        path.colorGradient = template.colorGradient;

        var points = new Vector3[] { Vector3.zero - fromTo, Vector3.zero };
        path.SetPositions(points);

        path.useWorldSpace = false;
    }
}
