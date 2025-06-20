 using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class UI_PostBoard : MonoBehaviour
{
    public Button WriteButton;

    private List<UI_PostSlot> _slots;

    [SerializeField]
    private GameObject _prefabPostSlot;

    [SerializeField]
    private VerticalLayoutGroup _verticalLayoutGroup;

    private void Awake()
    {
        InitSlot();
    }

    private void Start()
    {
        Refresh();
        PostManager.Instance.OnDataChanged += ((postId) => Refresh(postId));
        PostManager.Instance.OnDataAdded += Refresh;
        PostManager.Instance.OnDataDeleted += ((postId) => DeleteSlot(postId));

        WriteButton.onClick.AddListener(() => UIManager.Instance.OnClickWriteButton());
    }


    private void AdjustSlot(int postCount)
    {
        if (_slots.Count < postCount)
        {
            while (_slots.Count < postCount)
            {
                AddSlot();
            }
        }
        else if (postCount < _slots.Count)
        {
            RemoveSlot();
        }
    }

    private void Refresh()
    {
        List<PostDTO> postList = PostManager.Instance.Posts;
        AdjustSlot(postList.Count);

        for (int i = 0; i < postList.Count; i++)
        {
            _slots[i].Refresh(postList[i]);
        }
    }

    private void Refresh(string postId)
    {
        List<PostDTO> postList = PostManager.Instance.Posts;
        AdjustSlot(postList.Count);

        for (int i = 0; i < postList.Count; i++)
        {
            if (postList[i].ID.Equals(postId))
            {
                _slots[i].Refresh(postList[i]);
                return;
            }
        }
    }

    private void DeleteSlot(string postId)
    {
        _slots.Remove(_slots.Find(slot => slot.PostDto.ID == postId));
    }


    private void InitSlot()
    {
        _slots = _verticalLayoutGroup.gameObject.GetComponentsInChildren<UI_PostSlot>(true).ToList();
    }

    private void AddSlot()
    {
        if (int.TryParse(_slots[_slots.Count - 1].name.Split('_')[0], out int lastSlotNumber))
        {
            GameObject slot = Instantiate(_prefabPostSlot, _verticalLayoutGroup.transform);
            slot.name = $"{_prefabPostSlot.name}_{lastSlotNumber}";
            Debug.Log($"LastSlotNumber : {lastSlotNumber}");
            _slots.Add(slot.GetComponent<UI_PostSlot>());
        }
    }

    private void RemoveSlot()
    {
        Destroy(_slots[_slots.Count - 1].gameObject);
    }
}
