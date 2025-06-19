using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PostManager : MonoBehaviourSingleton<PostManager>
{
    private List<Post> _posts;
    public List<PostDTO> Posts => _posts.ConvertAll((a) => new PostDTO(a));

    private PostRepository _repository;

    public event Action<string> OnDataChanged;


    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        _repository = new PostRepository();
    }

    public async Task AddPost(PostDTO postDto)
    {
        await _repository.AddPost(postDto);
        OnDataChanged?.Invoke(postDto.ID);
    }

    public async Task UpdatePost(PostDTO postDto)
    {
        await _repository.UpdatePost(postDto);
        OnDataChanged?.Invoke(postDto.ID);
    }

    public async Task DeletePost(string postId)
    {
        await _repository.DeletePost(postId);
        OnDataChanged?.Invoke(postId);
    }

    public async Task TryLikePost(string postId)
    {
        string email = AccountManager.Instance.CurrentAcount.Email;
        if (await _repository.TryLikePost(postId, email))
        {
            OnDataChanged?.Invoke(postId);
        }
    }
}
