using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField]
    private UI_PostMenu _postMenu;

    [SerializeField]
    private UI_PostRead _postRead;

    [SerializeField]
    private UI_PostWrite _postWrite;

    [SerializeField]
    private UI_PostModify _postModify;

    public event Action OnDeletePost;


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
        _postMenu.transform.SetParent(clickedButtonRectTransform);
        _postMenu.transform.localPosition = new Vector3(-clickedButtonRectTransform.rect.width, 0, 0);
        

        _postMenu.Refresh(selectedPost);
        _postMenu.gameObject.SetActive(true);
    }

    public void OnClickReadButton(PostDTO selectedPost)
    {
        _postRead.Refresh(selectedPost);
        _postRead.gameObject.SetActive(true);
    }

    public void OnClickWriteButton()
    {
        _postWrite.gameObject.SetActive(true);
    }

    public void OnClickModifyButton(PostDTO selectedPost)
    {
        _postModify.Refresh(selectedPost);
        _postModify.gameObject.SetActive(true);
    }

    public void CloseAllTab()
    {
        OnDeletePost?.Invoke();
    }
}
