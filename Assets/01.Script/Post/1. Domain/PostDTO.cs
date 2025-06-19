using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
public class PostDTO
{
    [FirestoreDocumentId]
    public string ID { get; }

    [FirestoreProperty]
    public string Title { get; }

    [FirestoreProperty]
    public string Description { get; }

    [FirestoreProperty]
    public string Email { get; }

    [FirestoreProperty]
    public string Nickname { get; }

    [FirestoreProperty]
    public DateTime UploadTime { get; }

    [FirestoreProperty]
    public List<string> LikeAccounts { get; }

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