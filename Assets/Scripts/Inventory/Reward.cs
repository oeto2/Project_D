using DarkPixelRPGUI.Scripts.UI.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reward : MonoBehaviour
{

    public static bool rewardActivated = false;


    // ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    [SerializeField]
    private GameObject _rewardBase;
    [SerializeField]
    private GameObject _slotsParent;

    // ½½·Ôµé.
    private Slot[] slots;


    // Use this for initialization
    void Start()
    {
        slots = _slotsParent.GetComponentsInChildren<Slot>();
        AcquireItem(Database.Item.Get(20000001));
        AcquireItem(Database.Item.Get(20000002));
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
}
