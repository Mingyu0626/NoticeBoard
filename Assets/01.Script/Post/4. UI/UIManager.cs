using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField]
    private RectTransform _postMenuRectTransform;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        
    }

    public void OnClickBackButton(GameObject layout)
    {
        layout.SetActive(false);
    }

    public void OnClickMenuButton(RectTransform clickedButtonRectTransform)
    {
        _postMenuRectTransform.anchoredPosition = clickedButtonRectTransform.anchoredPosition;
        _postMenuRectTransform.gameObject.SetActive(true);
    }
}
