using TMPro;
using UnityEngine;

public class UI_ChatMessage : MonoBehaviour
{
    public EChatType ChatType;
    public TextMeshProUGUI NicknameTextUI;
    public TextMeshProUGUI MessageTextUI;
    // public TextMeshProUGUI DateTimeTextUI;

    public void Set(Chat chat)
    {
        if (NicknameTextUI != null)
        {
            NicknameTextUI.text = chat.Nickname;
        }
        if (MessageTextUI != null)
        {
            MessageTextUI.text = chat.Message;
        }
        // DateTimeTextUI.text = "¹Ì±¸Çö";
    }
}
