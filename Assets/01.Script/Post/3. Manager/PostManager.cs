using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PostManager : MonoBehaviourSingleton<PostManager>
{
    private List<Post> _posts;
    public List<PostDTO> Posts => _posts.ConvertAll((a) => new PostDTO(a));

    private PostRepository _repository;

    public event Action<PostDTO> OnDataChanged;
    public event Action OnDataAdded;
    public event Action<string> OnDataDeleted;

    private int _idNumber;


    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        
    }

    public async void Init()
    {
        _repository = new PostRepository();
        _posts = new List<Post>();
        List<PostDTO> postDtoList = await _repository.GetAllPosts();
        foreach (PostDTO dto in postDtoList)
        {
            _posts.Add(new Post(dto));
            if (int.TryParse(dto.ID, out int id))
            {
                _idNumber = Math.Max(id, _idNumber);
            }
        }
        OnDataAdded?.Invoke();
    }

    public string GetID()
    {
        _idNumber++;
        Debug.Log($"Id Number : {_idNumber}");
        return _idNumber.ToString();
    }

    public async Task AddPost(PostDTO postDto)
    {
        await _repository.AddPost(postDto);
        _posts.Add(new Post(postDto));
        OnDataAdded?.Invoke();
    }

    public async Task UpdatePost(PostDTO postDto)
    {
        await _repository.UpdatePost(postDto);
        _posts.Find(post => post.ID == postDto.ID).UpdatePost(postDto);
        OnDataChanged?.Invoke(postDto);
    }

    public async Task DeletePost(string postId)
    {
        await _repository.DeletePost(postId);
        _posts.Remove(_posts.Find(post => post.ID == postId));
        OnDataDeleted?.Invoke(postId);
    }

    public async Task TryLikePost(string postId)
    {
        string email = AccountManager.Instance.CurrentAcount.Email;
        if (await _repository.TryLikePost(postId, email))
        {
            Post post = _posts.Find(post => post.ID == postId);
            if (post == null)
            {
                return;
            }
            if (post.LikeAccounts.Contains(email))
            {
                post.DeleteLikeAccount(email);
            }
            else
            {
                post.AddLikeAccount(email);
            }
            OnDataChanged?.Invoke(post.ToDTO());
        }
    }
}
