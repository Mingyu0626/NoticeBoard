using System;
using System.Collections.Generic;
using UnityEngine;

public class PostDTO
{
    public readonly string ID;
    public readonly string Title;
    public readonly string Description;
    public readonly string Email;
    public readonly string Nickname;
    public readonly DateTime UploadTime;
    public readonly List<string> LikeAccounts;

    public PostDTO(Post notice)
    {
        ID = notice.ID;
        Title = notice.Title;
        Description = notice.Description;
        Email = notice.Email;
        Nickname = notice.Nickname;
        UploadTime = notice.UploadTime;
        LikeAccounts = notice.LikeAccounts;
    }
}
