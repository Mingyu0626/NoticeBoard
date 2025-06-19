using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PostManager : MonoBehaviourSingleton<PostManager>
{
    private List<Post> _posts;
    public List<PostDTO> Posts => _posts.ConvertAll((a) => new PostDTO(a));

    private PostRepository _repository;

    public event Action OnDataChanged;

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
        OnDataChanged?.Invoke();
    }

    public async Task UpdatePost(PostDTO postDto)
    {
        await _repository.UpdatePost(postDto);
        OnDataChanged?.Invoke();
    }

    public async Task DeletePost(string postId)
    {
        await _repository.DeletePost(postId);
        OnDataChanged?.Invoke();
    }

    public async Task TryLikePost(string postId)
    {
        if (await _repository.TryLikePost(postId))
        {
            OnDataChanged?.Invoke();
        }
    }
}
