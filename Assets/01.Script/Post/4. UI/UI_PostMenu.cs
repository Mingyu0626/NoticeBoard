using UnityEngine;
using UnityEngine.UI;

public class UI_PostMenu : MonoBehaviour
{
    public Button ModifyButton;
    public Button DeleteButton;

    private PostDTO _selectedPost;

    private void Start()
    {
        ModifyButton.onClick.AddListener(() => UIManager.Instance.OnClickModifyButton(_selectedPost));
        DeleteButton.onClick.AddListener(() => OnClickDeleteButton());
        UIManager.Instance.OnDeletePost += (() => gameObject.SetActive(false));
    }

    public void Refresh(PostDTO selectedPost)
    {
        _selectedPost = selectedPost;
    }

    private async void OnClickDeleteButton()
    {
        await PostManager.Instance.DeletePost(_selectedPost.ID);
        UIManager.Instance.CloseAllTab();
    }
}
