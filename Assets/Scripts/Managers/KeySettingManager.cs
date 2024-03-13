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

        // 키가 눌렸을 때, 버튼을 누른 후라면 작동
        if (keyEvent.isKey && keyBool)
        {
            keyBool = false;

            var Key = _playerInput.actions[$"NoteKey{index}"];
            var bindingIndex = Key.GetBindingIndex();

            var keyStr = keyEvent.keyCode.ToString();

            //var noteKey = changeInputKeyUI.notes;
            //for (int i = 0; i < noteKey.Count; i++)
            //{
            //    // 이미 같은 키가 있으면 교체
            //    if (i != index && noteKey[i].text == keyStr)
            //    {
            //        // 교체 할 인풋액션
            //        var tempKey = playerInput.actions[$"NoteKey{i}"];
            //        var tempStr = noteKey[index].text;

            //        // 버튼누른거 교체
            //        Key.ApplyBindingOverride(bindingIndex, $"<Keyboard>/{keyStr}");
            //        //changeInputKeyUI.UpdateUI(index, keyStr);

            //        // 중복 교체
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
