using System.Collections.Generic;
using UnityEngine;

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
        CleanRewardItem();
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

    //아이템 획득
    public void AcquireItem(ItemData item_, int count_ = 1)
    {
        if (item_.itemType == Constants.ItemType.Consume || item_.itemType == Constants.ItemType.Material)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == item_.itemName)
                    {
                        if (slots[i].itemCount + count_ > item_.itemMax_Stack)
                        {
                            int _overCount = slots[i].itemCount + count_ - item_.itemMax_Stack;
                            slots[i].SetSlotCount(count_);
                            count_ = _overCount;
                            continue;
                        }
                        slots[i].SetSlotCount(count_);
                        return;
                        //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                    }
                }
                else
                {
                    if (count_ > item_.itemMax_Stack)
                    {
                        slots[i].AddItem(item_, item_.itemMax_Stack);
                        count_ = count_ - item_.itemMax_Stack;
                        continue;
                    }
                    slots[i].AddItem(item_, count_);
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item_, count_);
                //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                return;
            }
        }

        var _warningPopup = UIManager.Instance.GetPopup(nameof(WarningPopup)).GetComponent<WarningPopup>();
        _warningPopup.SetWarningPopup("아이템이 가득찼습니다.");
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

    //아이템 모두 획득
    public void Getitall()
    {
        if (!UIManager.Instance.ExistPopup(nameof(InventoryPopup)))
        {
            UIManager.Instance.ShowPopup(nameof(InventoryPopup));
            return;
        }
        Inventory inventory = UIManager.Instance.GetPopupObject(nameof(InventoryPopup)).GetComponent<Inventory>();
        // for (int i =0;i< slots.Length;i++)
        // {
        //     if (slots[i].item != null)
        //     {
        //         bool invenEmpty = inventory.AcquireItem(slots[i].item, slots[i].itemCount);
        //         if(invenEmpty)
        //             slots[i].ClearSlot();
        //     }
        // }
        GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
        GameManager.Instance.CallUpdateRewardCountEvent();
        
        Debug.Log("아이템 모두 획득");
    }
}
