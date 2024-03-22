using DarkPixelRPGUI.Scripts.UI.Equipment;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    public static bool inventoryActivated = false;
    public static Inventory instance = null;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _inventoryBase;
    [SerializeField]
    private GameObject _slotsParent;

    // 슬롯들.
    private Slot[] _slots;

    public TMP_Text goldText;

    
    // Use this for initialization
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        UpdateGold(0);
    }

    private void Start()
    {
        _slots = _slotsParent.GetComponentsInChildren<Slot>();
        if(InformationManager.Instance.slots != null)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i] = InformationManager.Instance.slots[i];
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q)) 
        {
            Debug.Log("Q누름");
            AcquireItem(Database.Item.Get(20000001));
            AcquireItem(Database.Item.Get(20000002));
            AcquireItem(Database.Item.Get(30000002));
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
                return;
            }
        }
    }

    public void UpdateGold(int itemPrice_)
    {
        InformationManager.Instance.gold -= itemPrice_;
        goldText.text = InformationManager.Instance.gold.ToString();
    }

    
    private void OnDestroy()
    {
        if (InformationManager.Instance.slots == null)
            InformationManager.Instance.slots = _slots;
        for (int i = 0; i < _slots.Length; i++)
        {
            InformationManager.Instance.slots[i] = _slots[i]; 
        }
    }

}
