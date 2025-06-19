using Firebase.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PostRepository
{
    public void Save(PostDTO noticeDto)
    {
        Post notice = new Post(noticeDto);
        Dictionary<string, object> noticeDict = new Dictionary<string, object>
            {
                { "Title", noticeDto.Title },
                { "Description", noticeDto.Description },
                { "Email", noticeDto.Email },
                { "Nickname", noticeDto.Nickname },
                { "UploadTime", noticeDto.UploadTime.ToString() },
                { "LikeAccounts", noticeDto.LikeAccounts }
            };

        FirebaseManager.Instance.Database.Collection("noticeboard")
            .Document(noticeDto.ID).
            SetAsync(noticeDict).
            ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log($"게시글 추가 완료, ID : {noticeDto.ID}");
                }
                else
                {
                    Debug.LogError($"게시글 추가 실패: {task.Exception}");
                }
            });
    }

    //public List<NoticeDTO> Load()
    //{

    //}
}


