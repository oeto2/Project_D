using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    public static bool inventoryActivated = false;

    // « ø‰«— ƒƒ∆˜≥Õ∆Æ
    [SerializeField]
    private GameObject _inventoryBase;
    [SerializeField]
    private GameObject _slotsParent;

    // ΩΩ∑‘µÈ.
    private Slot[] _slots;

    public TMP_Text goldText;


    // Use this for initialization


    private void Awake()
    {
        InformationManager.Instance.InvenGoldUpdate += UpdateGold;
        InformationManager.Instance.InvenGoldChange(0);
        GameManager.Instance.player.Stats.OnDie += ClearInventory;
    }

    private void Start()
    {
         _slots = _slotsParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < _slots.Length; i++)
        {
            if (InformationManager.Instance.saveLoadData.itemID[i] != 0)
            {
                _slots[i].AddItem(Database.Item.Get(InformationManager.Instance.saveLoadData.itemID[i]),
                    InformationManager.Instance.saveLoadData.itemStack[i]);
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

    private void ClearInventory()
    {
        foreach(var item in _slots)
        {
            item.ClearSlot();
        }
        InformationManager.Instance.InvenGoldChange(-InformationManager.Instance.saveLoadData.gold);
    }

    private void ToggleInventory()
    {
        inventoryActivated = !inventoryActivated;
        if (inventoryActivated)
            OpenInventory();
        else
            CloseInventory();
    }

    private void OpenInventory()
    {
        _inventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        _inventoryBase.SetActive(false);
    }

    public bool AcquireItem(ItemData item_, int count_ = 1)
    {
        if (item_.itemType == Constants.ItemType.Consume || item_.itemType == Constants.ItemType.Material)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i].item != null)
                {
                    if (_slots[i].item.itemName == item_.itemName)
                    {
                        if (_slots[i].itemCount + count_ > item_.itemMax_Stack)
                        {
                            int _overCount = _slots[i].itemCount + count_ - item_.itemMax_Stack;
                            _slots[i].SetSlotCount(count_);
                            count_ = _overCount;
                            continue;
                        }
                        _slots[i].SetSlotCount(count_);
                        return true;
                        //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                    }
                }
                else
                {
                    if (count_ > item_.itemMax_Stack)
                    {
                        _slots[i].AddItem(item_, item_.itemMax_Stack);
                        count_ = count_ - item_.itemMax_Stack;
                        continue;
                    }
                    _slots[i].AddItem(item_, count_);
                    return true;
                }
            }
        }

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].item == null)
            {
                _slots[i].AddItem(item_, count_);
                //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                return true;
            }
        }
        var _warningPopup = UIManager.Instance.GetPopup(nameof(WarningPopup)).GetComponent<WarningPopup>();
        _warningPopup.SetWarningPopup("æ∆¿Ã≈€¿Ã ∞°µÊ√°Ω¿¥œ¥Ÿ.");
        return false;
    }

    public void UpdateGold(int itemPrice_)
    {
        goldText.text = InformationManager.Instance.saveLoadData.gold.ToString();
    }

    public void InvenToStorage()
    {
        if (UIManager.Instance.GetPopupObject("StoragePopup") != null)
        {
            if (UIManager.Instance.GetPopupObject("StoragePopup").activeSelf)
            {
                int gold = InformationManager.Instance.saveLoadData.gold;
                InformationManager.Instance.InvenGoldChange(-gold);
                InformationManager.Instance.StorageGoldChange(gold);
            }
        }
    }

    private void OnDisable()
    {
        InformationManager.Instance.SaveInformation(_slots);
    }
}
