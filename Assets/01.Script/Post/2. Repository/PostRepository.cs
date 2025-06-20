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
            // PostDTO 객체를 직접 전달하여 Firestore에 저장
            await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .Document(postDto.ID)
                .SetAsync(postDto);

            Debug.Log($"게시글 추가 완료, ID : {postDto.ID}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"게시글 추가 실패: {ex}");
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
            Debug.LogError($"게시글 목록 조회 실패: {ex}");
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
            Debug.LogError($"게시글 목록 조회 실패: {ex}");
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
                // DocumentSnapshot을 PostDTO 객체로 바로 역직렬화
                return snapshot.ConvertTo<PostDTO>();
            }
            else
            {
                Debug.LogWarning($"해당 ID의 게시글 없음: {postId}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"게시글 조회 실패: {ex}");
        }

        return null;
    }

    public async Task UpdatePost(PostDTO postDto)
    {
        try
        {
            // PostDTO 객체를 사용하여 문서 업데이트 (기존 필드 병합)
            await FirebaseManager.Instance.Database
                .Collection("noticeboard")
                .Document(postDto.ID)
                .SetAsync(postDto, SetOptions.MergeAll);

            Debug.Log($"게시글 수정 완료: {postDto.ID}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"게시글 수정 실패: {ex}");
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

            Debug.Log($"게시글 삭제 완료: {postId}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"게시글 삭제 실패: {ex}");
        }
    }

    public async Task<bool> TryLikePost(string postId, string email)
    {
        Post post = new Post(await GetPost(postId));
        List<string> likeAccounts = post.LikeAccounts;
        if (likeAccounts.Contains(email))
        {
            Debug.LogWarning("이미 좋아요를 누르셨습니다.");
            return false;
        }
        post.AddLikeAccount(email);
        await AddPost(post.ToDTO());
        return true;
    }
}