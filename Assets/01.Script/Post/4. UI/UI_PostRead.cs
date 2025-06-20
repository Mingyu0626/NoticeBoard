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

    private void Start()
    {
        BackButton.onClick.AddListener(() => UIManager.Instance.OnClickBackButton(gameObject));
        MenuButton.onClick.AddListener(() => UIManager.Instance.OnClickMenuButton(GetComponent<RectTransform>()));
    }
}
