using Constants;
using DarkPixelRPGUI.Scripts.UI.Equipment;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Storage : MonoBehaviour
{

    public static bool storageActivated = false;
    public static Storage instance = null;

    // ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    [SerializeField]
    private GameObject _storageBase;
    [SerializeField]
    private GameObject _slotsParent;

    // ½½·Ôµé.
    private Slot[] _slots;

    public TMP_Text goldText;


    // Use this for initialization


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        InformationManager.Instance.StorageGoldUpdate += UpdateGold;
        InformationManager.Instance.StorageGoldChange(0);
    }

    private void Start()
    {
        _slots = _slotsParent.GetComponentsInChildren<Slot>();

        for (int i = 0; i < _slots.Length; i++)
        {
            if (InformationManager.Instance.saveLoadData.storage_ItemID[i] != 0)
            {
                _slots[i].AddItem(Database.Item.Get(InformationManager.Instance.saveLoadData.storage_ItemID[i]),
                    InformationManager.Instance.saveLoadData.storage_ItemStack[i]);
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


    private void ToggleInventory()
    {
        storageActivated = !storageActivated;
        if (storageActivated)
            OpenInventory();
        else
            CloseInventory();
    }

    private void OpenInventory()
    {
        _storageBase.SetActive(true);
    }

    private void CloseInventory()
    {
        _storageBase.SetActive(false);
    }

    public void AcquireItem(ItemData _item, int _count = 1)
    {
        if (_item.itemType == Constants.ItemType.Consume || _item.itemType == Constants.ItemType.Material)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i].item != null)
                {
                    if (_slots[i].item.itemName == _item.itemName)
                    {
                        _slots[i].SetSlotCount(_count);
                        //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].item == null)
            {
                _slots[i].AddItem(_item, _count);
                //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                return;
            }
        }
    }

    public void UpdateGold(int itemPrice_)
    {
        Debug.Log(InformationManager.Instance.saveLoadData.storage_Gold.ToString());
        goldText.text = InformationManager.Instance.saveLoadData.storage_Gold.ToString();
    }

    public void StorageToInven()
    {
        int gold = InformationManager.Instance.saveLoadData.storage_Gold;
        InformationManager.Instance.StorageGoldChange(-gold);
        InformationManager.Instance.InvenGoldChange(gold);
    }

    private void OnDisable()
    {
        InformationManager.Instance.SaveInformation(_slots);
    }
    //
    //
    //private void OnDestroy()
    //{
    //    if (InformationManager.Instance.saveLoadData.slots == null)
    //        InformationManager.Instance.saveLoadData.slots = _slots;
    //    for (int i = 0; i < _slots.Length; i++)
    //    {
    //        InformationManager.Instance.saveLoadData.slots[i] = _slots[i]; 
    //    }
    //}

}
