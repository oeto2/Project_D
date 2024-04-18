using DarkPixelRPGUI.Scripts.UI.Equipment;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reward : MonoBehaviour
{
    public static bool rewardActivated = false;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _rewardBase;

    [SerializeField]
    private GameObject _slotsParent;

    // 슬롯들.
    public Slot[] slots;

    private void Awake()
    {
        slots = _slotsParent.GetComponentsInChildren<Slot>();
    }

    private void Start()
    {
        GameManager.Instance.SetRewardItemEvent += GetCurrentItemsId;
    }

    private void OpenReward()
    {
        _rewardBase.SetActive(true);
    }

    private void CloseReward()
    {
        _rewardBase.SetActive(false);
    }

    public void AcquireItem(ItemData _item, int _count = 1)
    {
        if (_item.itemType == Constants.ItemType.Consume || _item.itemType == Constants.ItemType.Material)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    //현재 리워드 인벤에 있는 아이템목록 ID값 얻기
    public List<int> GetCurrentItemsId()
    {
        List<int> itemsId = new List<int>();

        foreach (var slot in slots)
        {
            if (slot.item != null)
            {
                itemsId.Add(slot.item.id);
            }
        }

        return itemsId;
    }

    //보상 아이템 비우기
    public void CleanRewardItem()
    {
        Debug.Log("아이템 비우기");
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].ClearSlot();
            }
        }
    }

    //해당되는 슬롯의 아이템 갯수 반환
    public int GetItemCountInSlot(int slotNum_)
    {
        return slots[slotNum_].itemCount;
    }


    public void Getitall()
    {
        Inventory inventory = UIManager.Instance.GetPopupObject(nameof(InventoryPopup)).GetComponent<Inventory>();
        for (int i =0;i< slots.Length;i++)
        {
            if (slots[i].item != null)
            {
                inventory.AcquireItem(slots[i].item, slots[i].itemCount);
            }
        }
        CleanRewardItem();
    }
}
