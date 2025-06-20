using UnityEngine;
using UnityEngine.UI;

public class UI_PostMenu : MonoBehaviour
{
    public Button ModifyButton;
    public Button DeleteButton;

    private PostDTO _selectedPost;

    private void Start()
    {
        ModifyButton.onClick.AddListener(() => OnClickModifyButton());
        DeleteButton.onClick.AddListener(() => OnClickDeleteButton());
    }

    public void Refresh(PostDTO selectedPost)
    {
        _selectedPost = selectedPost;
    }

    private void OnClickModifyButton()
    {

    }

    private void OnClickDeleteButton()
    {

    }
}
