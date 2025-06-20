using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Firestore;

[FirestoreData]
public class PostDTO
{
    [FirestoreDocumentId]
    public string ID { get; private set; }

    [FirestoreProperty]
    public string Title { get; private set; }

    [FirestoreProperty]
    public string Description { get; private set; }

    [FirestoreProperty]
    public string Email { get; private set; }

    [FirestoreProperty]
    public string Nickname { get; private set; }

    [FirestoreProperty]
    public DateTime UploadTime { get; private set; }

    [FirestoreProperty]
    public List<string> LikeAccounts { get; private set; }

    public PostDTO()
    {
        
        LikeAccounts = new List<string>();
    }

    public PostDTO(Post notice)
    {
        ID = notice.ID;
        Title = notice.Title;
        Description = notice.Description;
        Email = notice.Email;
        Nickname = notice.Nickname;
        UploadTime = notice.UploadTime;
        LikeAccounts = new List<string>(notice.LikeAccounts ?? new List<string>());
    }
}