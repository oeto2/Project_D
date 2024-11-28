using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public static bool rewardActivated = false;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GameObject _rewardBase;

    [SerializeField]
    private GameObject _slotsParent;

    // ���Ե�.
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

    //������ ȹ��
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
        _warningPopup.SetWarningPopup("�������� ����á���ϴ�.");
    }

    //���� ������ �κ��� �ִ� �����۸�� ID�� ���
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

    //���� ������ ����
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

    //�ش�Ǵ� ������ ������ ���� ��ȯ
    public int GetItemCountInSlot(int slotNum_)
    {
        return slots[slotNum_].itemCount;
    }

    //������ ��� ȹ��
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
        
        Debug.Log("������ ��� ȹ��");
    }
}
