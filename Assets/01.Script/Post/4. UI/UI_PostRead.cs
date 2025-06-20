using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostRead : MonoBehaviour
{
    public Button BackButton;
    public Button MenuButton;

    public TextMeshProUGUI TitleTMP;
    public TextMeshProUGUI NicknameTMP;
    public TextMeshProUGUI UploadTimeTMP;
    public TextMeshProUGUI DescriptionTMP;
    public TextMeshProUGUI LikeCountTMP;

    private PostDTO _post;

    private void Start()
    {
        BackButton.onClick.AddListener(() => UIManager.Instance.OnClickBackButton(gameObject));
        MenuButton.onClick.AddListener(() => UIManager.Instance.OnClickMenuButton(GetComponent<RectTransform>(), _post));
    }

    public void Refresh(PostDTO postDto)
    {
        _post = postDto;
        TitleTMP.SetText(postDto.Title);
        NicknameTMP.SetText(postDto.Nickname);
        UploadTimeTMP.SetText(postDto.UploadTime.ToString());
        DescriptionTMP.SetText(postDto.Description);
        LikeCountTMP.SetText(postDto.LikeAccounts.Count.ToString());
    }
}
