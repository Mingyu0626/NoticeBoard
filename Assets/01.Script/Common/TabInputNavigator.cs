using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabInputNavigator : MonoBehaviour
{
    [SerializeField]
    private List<TMP_InputField> _inputFields;

    [SerializeField]
    private Button               _confirmButton;

    private void Update()
    {
        if (_inputFields.Count <= 0) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _confirmButton.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;
            int selectedIndex = _inputFields.FindIndex(f => f.gameObject == current);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                selectedIndex = (selectedIndex <= 0) ? _inputFields.Count - 1 : selectedIndex - 1;
            }
            else
            {
                selectedIndex = (selectedIndex + 1) % _inputFields.Count;
            }

            _inputFields[selectedIndex].Select();
        }
    }
}
