using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventTest : MonoBehaviour
{
    private void Update()
    {
        //인벤토리 열기
        if(Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.ShowPopup<EquipmentPopup>();
            UIManager.Instance.ShowPopup<InventoryPopup>();
            UIManager.Instance.ShowPopup<DragPopup>();
        }

        //보상창 열기 열기
        if (Input.GetKeyDown(KeyCode.N))
        {
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.ShowPopup<RewardPopup>();
        }
    }
}
