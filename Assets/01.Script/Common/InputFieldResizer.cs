using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldResizer : MonoBehaviour
{
    public TMP_InputField inputField;
    public LayoutElement layoutElement;
    public float minHeight = 40f;
    public float maxHeight = 300f;
    public float padding = 10f;

    private void Start()
    {
        inputField.onValueChanged.AddListener(Resize);
        Resize(inputField.text);
    }

    private void Resize(string text)
    {
        float preferredHeight = inputField.textComponent.GetPreferredValues(text).y + padding;
        preferredHeight = Mathf.Clamp(preferredHeight, minHeight, maxHeight);
        layoutElement.preferredHeight = preferredHeight;
    }
}
