using DarkPixelRPGUI.Scripts.UI.Equipment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory2 : MonoBehaviour
{
    public static bool inventoryActivated = false;

    [SerializeField]
    private GameObject _inventoryBase;
    [SerializeField]
    private GameObject _inventorySlots;

    private Slot2[] _slots;
    // Start is called before the first frame update
    void Start()
    {
        _slots = GetComponentsInChildren<Slot2>();
    }
    
    private void ToggleInventory()
    {
        inventoryActivated = !inventoryActivated;
        if (inventoryActivated)
            OpenInventory();
        else
            CloseInventory();
    }

    private void CloseInventory()
    {
        _inventoryBase.SetActive(false);
    }

    private void OpenInventory()
    {
        _inventoryBase.SetActive(true);
    }

    public void AcquireItem(Item _item,int _count=1)
    {
        //if(아이템타입확인)
        for(int i = 0;i<_slots.Length;i++)
        {
            //아이템이 똑같으면 count가 늘어나도록설정
            return;
        }


        for(int i=0;i<_slots.Length;i++)
        {
            if (_slots[i].item == null)
            {
                _slots[i].Additem(_item, _count);
                return;
            }
        }
    }
    public void OnInventoryInput(InputAction.CallbackContext callbackcontext)
    {
        if (callbackcontext.phase == InputActionPhase.Started)
        {
            ToggleInventory();
        }
    }
}
