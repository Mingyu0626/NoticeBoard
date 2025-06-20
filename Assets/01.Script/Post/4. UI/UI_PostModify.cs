using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PostModify : MonoBehaviour
{
    private PostDTO _post;

    public Button BackButton;
    public Button WriteButton;

    public TMP_InputField TitleInputField;
    public TMP_InputField DescriptionInputField;

    private void Start()
    {
        BackButton.onClick.AddListener(() => UIManager.Instance.OnClickBackButton(gameObject));
        WriteButton.onClick.AddListener(() => OnClickCompleteButton());
    }

    public void Refresh(PostDTO post)
    {
        _post = post;

        TitleInputField.text = post.Title;
        DescriptionInputField.text = post.Description;
    }

    private async void OnClickCompleteButton()
    {
        Post post = new Post
            (_post.ID,
            TitleInputField.text,
            DescriptionInputField.text,
            _post.Email,
            _post.Nickname,
            DateTime.Now,
            _post.LikeAccounts
            );

        await PostManager.Instance.UpdatePost(post.ToDTO());
    }


}
