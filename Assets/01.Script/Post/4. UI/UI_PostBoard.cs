 using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class UI_PostBoard : MonoBehaviour
{
    public Button WriteButton;
    public GameObject PostSlotPrefab;
    public VerticalLayoutGroup VerticalLayoutGroup;

    private List<UI_PostSlot> _slots;

    private void Awake()
    {
        InitSlot();
    }

    private void Start()
    {
        PostManager.Instance.OnDataChanged += Refresh;
        PostManager.Instance.OnDataAdded += Refresh;
        PostManager.Instance.OnDataDeleted += ((postId) => DeleteSlot(postId));

        WriteButton.onClick.AddListener(() => UIManager.Instance.OnClickWriteButton());
    }


    private void AdjustSlot(int postCount)
    {
        int currentSlotCount = _slots.Count;
        if (currentSlotCount < postCount)
        {
            for (int i = currentSlotCount; i < postCount; i++)
            {
                AddSlot();
            }
        }
        else if (postCount < currentSlotCount)
        {

            for (int i = currentSlotCount; i > postCount; i--)
            {
                RemoveSlot();
            }
        }
    }

    private void Refresh()
    {
        Debug.Log("Refresh()");
        List<PostDTO> postList = PostManager.Instance.Posts;
        AdjustSlot(postList.Count);

        for (int i = 0; i < postList.Count; i++)
        {
            _slots[i].Refresh(postList[i]);
        }
    }

    private void Refresh(PostDTO post)
    {
        Debug.Log("Refresh(postId)");
        List<PostDTO> postList = PostManager.Instance.Posts;
        AdjustSlot(postList.Count);

        for (int i = 0; i < postList.Count; i++)
        {
            if (postList[i].ID.Equals(post.ID))
            {
                Debug.Log("id�� ������ ���� �߰�");
                Debug.Log(postList[i].LikeAccounts.Count);
                _slots[i].Refresh(postList[i]);
                return;
            }
        }
    }

    private void InitSlot()
    {
        _slots = new List<UI_PostSlot>();
    }

    private void AddSlot()
    {
        if (_slots.Count == 0)
        {
            GameObject slot = Instantiate(PostSlotPrefab, VerticalLayoutGroup.transform);
            slot.name = $"{PostSlotPrefab.name}_0";
            _slots.Add(slot.GetComponent<UI_PostSlot>());
        }
        else if (int.TryParse(_slots[_slots.Count - 1].name.Split('_')[2], out int lastSlotNumber))
        {
            GameObject slot = Instantiate(PostSlotPrefab, VerticalLayoutGroup.transform);
            slot.name = $"{PostSlotPrefab.name}_{lastSlotNumber}";
            _slots.Add(slot.GetComponent<UI_PostSlot>());
        }
    }

    private void RemoveSlot()
    {
        Destroy(_slots[_slots.Count - 1].gameObject);
    }

    private void DeleteSlot(string postId)
    {
        _slots.Remove(_slots.Find(slot => slot.PostDto.ID == postId));
    }
}
