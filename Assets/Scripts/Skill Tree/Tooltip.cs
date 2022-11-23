using TMPro;
using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    [SerializeField] int warpLimit = 240;

    public TextMeshProUGUI headerField;

    public TextMeshProUGUI descriptionField;

    public LayoutElement layoutElement;

    public void SetText(string header, string desc)
    {
        headerField.text = header;
        descriptionField.text = desc;

        layoutElement.enabled = Mathf.Max(header.Length, desc.Length) > warpLimit;
    }
}
