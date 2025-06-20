using UnityEngine;
using UnityEngine.UI;

public class UI_PostMenu : MonoBehaviour
{
    public Button ModifyButton;
    public Button DeleteButton;

    private void Start()
    {
        ModifyButton.onClick.AddListener(() => OnClickModifyButton());
        DeleteButton.onClick.AddListener(() => OnClickDeleteButton());
    }


    private void OnClickModifyButton()
    {

    }

    private void OnClickDeleteButton()
    {

    }
}
