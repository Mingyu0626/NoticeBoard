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
        List<string> test = new List<string>{ "��", "��", "��", "��", "��" };
        Post notice = new Post("4", "�׽�Ʈ1", "�׽�Ʈ2", "mg010626@naver.com", "�ֹα�ī��", DateTime.Now, test);
        _repository.Save(notice.ToDTO());
    }
}
