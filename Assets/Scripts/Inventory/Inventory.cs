using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    public static bool inventoryActivated = false;
    public static Inventory instance = null;

    // ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    [SerializeField]
    private GameObject _inventoryBase;
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
        UpdateGold(0);
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            AcquireItem(Database.Item.Get(20000001));
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

    public void AcquireItem(ItemData item_, int count_ = 1)
    {
        if (item_.itemType == Constants.ItemType.Consume || item_.itemType == Constants.ItemType.Material)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i].item != null)
                {
                    if (_slots[i].item.itemName == item_.itemName)
                    {
                        _slots[i].SetSlotCount(count_);
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
                _slots[i].AddItem(item_, count_);
                //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                return;
            }
        }
    }

    public void UpdateGold(int itemPrice_)
    {
        InformationManager.Instance.SaveInformation(itemPrice_);
        goldText.text = InformationManager.Instance.saveLoadData.gold.ToString();
    }


    private void OnDisable()
    {
        InformationManager.Instance.SaveInformation(_slots);
    }
}
