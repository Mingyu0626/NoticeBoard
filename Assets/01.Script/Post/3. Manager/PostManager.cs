using System;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviourSingleton<PostManager>
{
    private List<Post> _notices;
    public List<PostDTO> Notices => _notices.ConvertAll((a) => new PostDTO(a));

    private PostRepository _repository;

    public event Action OnDataChanged;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        _repository = new PostRepository();
        List<string> test = new List<string>{ "아", "이", "우", "에", "오" };
        Post notice = new Post("4", "테스트1", "테스트2", "mg010626@naver.com", "최민규카츠", DateTime.Now, test);
        _repository.Save(notice.ToDTO());
    }
}
