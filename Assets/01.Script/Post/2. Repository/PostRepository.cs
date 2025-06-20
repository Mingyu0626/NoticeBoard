using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PostRepository
{
    public async Task AddPost(PostDTO postDto)
    {
        try
        {
            // PostDTO ��ü�� ���� �����Ͽ� Firestore�� ����
            await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .Document(postDto.ID)
                .SetAsync(postDto);

            Debug.Log($"�Խñ� �߰� �Ϸ�, ID : {postDto.ID}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"�Խñ� �߰� ����: {ex}");
        }
    }

    public async Task<List<PostDTO>> GetAllPosts()
    {
        List<PostDTO> postList = new List<PostDTO>();

        try
        {
            QuerySnapshot snapshot = await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .OrderByDescending("UploadTime")
                .GetSnapshotAsync();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    PostDTO post = doc.ConvertTo<PostDTO>();
                    postList.Add(post);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"�Խñ� ��� ��ȸ ����: {ex}");
        }

        return postList;
    }

    public async Task<List<PostDTO>> GetPosts(int limit)
    {
        List<PostDTO> postList = new List<PostDTO>();

        try
        {
            QuerySnapshot snapshot = await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .OrderByDescending("UploadTime")
                .Limit(limit)
                .GetSnapshotAsync();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    PostDTO post = doc.ConvertTo<PostDTO>();
                    postList.Add(post);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"�Խñ� ��� ��ȸ ����: {ex}");
        }

        return postList;
    }

    public async Task<PostDTO> GetPost(string postId)
    {
        try
        {
            DocumentSnapshot snapshot = await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .Document(postId)
                .GetSnapshotAsync();

            if (snapshot.Exists)
            {
                // DocumentSnapshot�� PostDTO ��ü�� �ٷ� ������ȭ
                return snapshot.ConvertTo<PostDTO>();
            }
            else
            {
                Debug.LogWarning($"�ش� ID�� �Խñ� ����: {postId}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"�Խñ� ��ȸ ����: {ex}");
        }

        return null;
    }

    public async Task UpdatePost(PostDTO postDto)
    {
        try
        {
            // PostDTO ��ü�� ����Ͽ� ���� ������Ʈ (���� �ʵ� ����)
            await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .Document(postDto.ID)
                .SetAsync(postDto, SetOptions.MergeAll);

            Debug.Log($"�Խñ� ���� �Ϸ�: {postDto.ID}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"�Խñ� ���� ����: {ex}");
        }
    }

    public async Task DeletePost(string postId)
    {
        try
        {
            await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .Document(postId)
                .DeleteAsync();

            Debug.Log($"�Խñ� ���� �Ϸ�: {postId}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"�Խñ� ���� ����: {ex}");
        }
    }

    public async Task<bool> TryLikePost(string postId, string email)
    {
        Post post = new Post(await GetPost(postId));
        List<string> likeAccounts = post.LikeAccounts;
        if (likeAccounts.Contains(email))
        {
            Debug.LogWarning("�̹� ���ƿ並 �����̽��ϴ�.");
            return false;
        }
        post.AddLikeAccount(email);
        await AddPost(post.ToDTO());
        return true;
    }
}