using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Chat : MonoBehaviour
{
    public TMP_InputField InputField;
    private List<UI_ChatMessage> _chatMessageUIList = new List<UI_ChatMessage>();

    [Header("프리팹")]
    public UI_ChatMessage MinePrefab;
    public UI_ChatMessage OtherPrefab;
    public UI_ChatMessage SystemPrefab;


    private void Start()
    {
        ChatManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        var chatList = ChatManager.Instance.ChatList;

        // UI를 다 지운다.
        foreach (var chatUi in _chatMessageUIList)
        {
            Destroy(chatUi.gameObject);
        }

        _chatMessageUIList.Clear();
        // 챗 타입에 따라 다른 프리팹을 생성하여 스크롤뷰 내 Content에 넣어준다.
        foreach (var chat in chatList)
        {
            UI_ChatMessage uiChatMessage = null;
            switch (chat.Type)
            {
                case EChatType.Mine:
                    uiChatMessage = Instantiate(MinePrefab, transform);
                    break;
                case EChatType.Other:
                    uiChatMessage = Instantiate(OtherPrefab, transform);
                    break;
                case EChatType.System:
                    uiChatMessage = Instantiate(SystemPrefab, transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            uiChatMessage.Set(chat);
        }
    }

    public void OnClickSendButton()
    {
        string text = InputField.text;

        if (!string.IsNullOrEmpty(text))
        {
            ChatManager.Instance.SendChatMessage(text);
            InputField.text = string.Empty;
        }
    }
}
