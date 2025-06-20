using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostSlot : MonoBehaviour
{
    private PostDTO _postDto;
    public PostDTO PostDto;

    public TextMeshProUGUI TitleTMP;
    public TextMeshProUGUI NicknameTMP;
    public TextMeshProUGUI UploadTimeTMP;
    public TextMeshProUGUI DescriptionTMP;
    public TextMeshProUGUI LikeCountTMP;

    public Button MenuButton;
    public Button LikeButton;

    private void Start()
    {
        SetOnClickListener();
    }

    private void SetOnClickListener()
    {
        MenuButton.onClick.AddListener
            (() => UIManager.Instance.OnClickMenuButton(MenuButton.gameObject.GetComponent<RectTransform>(), _postDto));
        LikeButton.onClick.AddListener
            (async () => await PostManager.Instance.TryLikePost(_postDto.ID));
    }

    public void Refresh(PostDTO postDto)
    {
        _postDto = postDto;
        TitleTMP.SetText(postDto.Title);
        NicknameTMP.SetText(postDto.Nickname);
        UploadTimeTMP.SetText(postDto.UploadTime.ToString());
        DescriptionTMP.SetText(postDto.Description);
    }
}
