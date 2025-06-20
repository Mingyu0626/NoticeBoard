using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostWrite : MonoBehaviour
{
    public Button BackButton;
    public Button CompleteButton;

    public TMP_InputField TitleInputField;
    public TMP_InputField DescriptionInputField;

    private void OnEnable()
    {
        TitleInputField.text = string.Empty;
        DescriptionInputField.text = string.Empty;
    }

    private void Start()
    {
        BackButton.onClick.AddListener(() => UIManager.Instance.OnClickBackButton(gameObject));
        CompleteButton.onClick.AddListener(() => OnClickCompleteButton());
    }

    private async void OnClickCompleteButton()
    {
        Post post = new Post
            (PostManager.Instance.GetID(),
            TitleInputField.text,
            DescriptionInputField.text,
            AccountManager.Instance.CurrentAcount.Email,
            AccountManager.Instance.CurrentAcount.Nickname,
            DateTime.Now,
            new List<string>()
            );

        await PostManager.Instance.AddPost(post.ToDTO());
        gameObject.SetActive(false);
    }
}
