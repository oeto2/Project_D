using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventTest : MonoBehaviour
{
    private void Update()
    {
        //�κ��丮 ����
        if(Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.ShowPopup<EquipmentPopup>();
            UIManager.Instance.ShowPopup<InventoryPopup>();
            UIManager.Instance.ShowPopup<DragPopup>();
        }

        //����â ���� ����
        if (Input.GetKeyDown(KeyCode.N))
        {
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.ShowPopup<RewardPopup>();
        }
    }
}
