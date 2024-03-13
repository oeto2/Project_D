using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeySettingManager : SingletoneBase<KeySettingManager>
{
    private PlayerInput _playerInput;
    //private ChangeInputKeyUI changeInputKeyUI;

    private bool keyBool;
    private int index;

    private void Start()
    {
        //changeInputKeyUI = GetComponent<ChangeInputKeyUI>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void ChangeKey(int _index)
    {
        keyBool = true;
        index = _index;
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;

        // Ű�� ������ ��, ��ư�� ���� �Ķ�� �۵�
        if (keyEvent.isKey && keyBool)
        {
            keyBool = false;

            var Key = _playerInput.actions[$"NoteKey{index}"];
            var bindingIndex = Key.GetBindingIndex();

            var keyStr = keyEvent.keyCode.ToString();

            //var noteKey = changeInputKeyUI.notes;
            //for (int i = 0; i < noteKey.Count; i++)
            //{
            //    // �̹� ���� Ű�� ������ ��ü
            //    if (i != index && noteKey[i].text == keyStr)
            //    {
            //        // ��ü �� ��ǲ�׼�
            //        var tempKey = playerInput.actions[$"NoteKey{i}"];
            //        var tempStr = noteKey[index].text;

            //        // ��ư������ ��ü
            //        Key.ApplyBindingOverride(bindingIndex, $"<Keyboard>/{keyStr}");
            //        //changeInputKeyUI.UpdateUI(index, keyStr);

            //        // �ߺ� ��ü
            //        bindingIndex = tempKey.GetBindingIndex();
            //        tempKey.ApplyBindingOverride(bindingIndex, $"<Keyboard>/{tempStr}");
            //        //changeInputKeyUI.UpdateUI(i, tempStr);

            //        return;
            //    }
            //}

            Key.ApplyBindingOverride(bindingIndex, $"<Keyboard>/{keyStr}");
            //changeInputKeyUI.UpdateUI(index, keyStr);
        }
    }
}
