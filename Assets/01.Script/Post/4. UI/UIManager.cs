using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField]
    private UI_PostMenu _postMenu;

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

    public void OnClickMenuButton(RectTransform clickedButtonRectTransform, PostDTO selectedPost)
    {
        _postMenu.GetComponent<RectTransform>().anchoredPosition = clickedButtonRectTransform.anchoredPosition;
        _postMenu.Refresh(selectedPost);
        _postMenu.gameObject.SetActive(true);
    }
}
