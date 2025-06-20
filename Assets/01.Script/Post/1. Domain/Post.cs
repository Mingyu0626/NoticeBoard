using System;
using System.Collections.Generic;
using UnityEngine;

public class Post
{
    public string ID;
    public string Title;
    public string Description;
    public string Email;
    public string Nickname;
    public DateTime UploadTime;
    public List<string> LikeAccounts;

    public Post(string id, string title, string description, string email, string nickname, DateTime uploadTime, List<string> likeAccounts)
    {
        var titleSpecification = new PostTitleSpecification();
        if (!titleSpecification.IsSatisfiedBy(title))
        {
            throw new Exception(titleSpecification.ErrorMessage);
        }

        var descriptionSpecification = new PostDescriptionSpecification();
        if (!descriptionSpecification.IsSatisfiedBy(description))
        {
            throw new Exception(descriptionSpecification.ErrorMessage);
        }

        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        var uploadTimeSpecification = new PostUploadTimeSpecification();
        if (!uploadTimeSpecification.IsSatisfiedBy(uploadTime))
        {
            throw new Exception(uploadTimeSpecification.ErrorMessage);
        }

        var likeAccountsSpecification = new PostLikeAccountsSpecification();
        if (!likeAccountsSpecification.IsSatisfiedBy((likeAccounts, email)))
        {
            throw new Exception(likeAccountsSpecification.ErrorMessage);
        }


        ID = id;
        Title = title;
        Description = description;
        Email = email;
        Nickname = nickname;
        UploadTime = uploadTime;
        LikeAccounts = likeAccounts;
    }

    public Post(PostDTO noticeDto)
    {
        ID = noticeDto.ID;
        Title = noticeDto.Title;
        Description = noticeDto.Description;
        Email = noticeDto.Email;
        Nickname = noticeDto.Nickname;
        UploadTime = noticeDto.UploadTime;
        LikeAccounts = noticeDto.LikeAccounts;
    }

    public PostDTO ToDTO()
    {
        return new PostDTO(this);
    }

    public void UpdatePost(PostDTO postDto)
    {
        var titleSpecification = new PostTitleSpecification();
        if (!titleSpecification.IsSatisfiedBy(postDto.Title))
        {
            throw new Exception(titleSpecification.ErrorMessage);
        }

        var descriptionSpecification = new PostDescriptionSpecification();
        if (!descriptionSpecification.IsSatisfiedBy(postDto.Description))
        {
            throw new Exception(descriptionSpecification.ErrorMessage);
        }

        Title = postDto.Title;
        Description = postDto.Description;
    }

    public void AddLikeAccount(string email)
    {
        LikeAccounts.Add(email);
    }
}
