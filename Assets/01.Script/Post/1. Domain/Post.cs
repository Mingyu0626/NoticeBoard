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
}
