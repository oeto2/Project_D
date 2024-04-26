using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public ItemData item; // ȹ���� ������.
    public int itemCount; // ȹ���� �������� ����.
    public Image itemImage; // �������� �̹���.
    //public int itemOriginPosX;
    //public int itemOriginPosY;

    // �ʿ��� ������Ʈ.
    [SerializeField]
    private TextMeshProUGUI _textCount;
    [SerializeField]
    private GameObject _countImage;

    // �̹����� ���� ����.
    private void SetColor(float alpha_)
    {
        Color color = itemImage.color;
        color.a = alpha_;
        itemImage.color = color;
    }

    // ������ ȹ��
    public void AddItem(ItemData item_, int count_ = 1)
    {
        item = item_;
        itemCount = count_;
        if(itemCount == 0)
        {
            ClearSlot();
            return;
        }
        itemImage.sprite = item.Sprite;

        if (item.itemType == Constants.ItemType.Material || item.itemType == Constants.ItemType.Consume)
        {
            _countImage.SetActive(true);
            _textCount.text = itemCount.ToString();
        }
        else
        {
            _textCount.text = "0";
            _countImage.SetActive(false);
        }

        SetColor(1);
    }

    // ������ ���� ����.
    public void SetSlotCount(int count_)
    {
        if(itemCount + count_ > item.itemMax_Stack)
        {
            itemCount = item.itemMax_Stack;
            _textCount.text = itemCount.ToString();
            if (UIManager.Instance.ExistPopup(nameof(BattleUI)))
            {
                for (int j = 0; j < 3; j++)
                {
                    if (UIManager.Instance.GetPopup(nameof(BattleUI)).GetComponent<BattleUI>().quickSlot[j].slot != null)
                        UIManager.Instance.GetPopup(nameof(BattleUI)).GetComponent<BattleUI>().quickSlot[j].SetSlotCount();
                }
            }
            return;
        }
        itemCount += count_;
        _textCount.text = itemCount.ToString();
        if (UIManager.Instance.ExistPopup(nameof(BattleUI)))
        {
            for (int j = 0; j < 3; j++)
            {
                if (UIManager.Instance.GetPopup(nameof(BattleUI)).GetComponent<BattleUI>().quickSlot[j].slot != null)
                    UIManager.Instance.GetPopup(nameof(BattleUI)).GetComponent<BattleUI>().quickSlot[j].SetSlotCount();
            }
        }
        if (itemCount <= 0)
            ClearSlot();
    }

    // ���� �ʱ�ȭ.
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        _textCount.text = "0";
        _countImage.SetActive(false);
    }

    public void UseItem()
    {
        GameManager.Instance.UsePotion(item);
        SetSlotCount(-1);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Constants.ItemType.Consume)
                    UseItem();
                else if (item.itemType == Constants.ItemType.Weapon)
                {
                    WeaponSlot _weaponSlot = UIManager.Instance.GetPopup(nameof(EquipmentPopup)).GetComponentInChildren<WeaponSlot>();
                    //��ȯ�� ������ ����
                    ItemData _tempItem = _weaponSlot.item;
                    _weaponSlot.ClearSlot();
                    _weaponSlot.AddItem(item);
                    if (_tempItem != null)
                        AddItem(_tempItem);
                    else
                        ClearSlot();
                }
                else if (item.itemType == Constants.ItemType.Helmet)
                {
                    HelmetSlot _helmetSlot = UIManager.Instance.GetPopup(nameof(EquipmentPopup)).GetComponentInChildren<HelmetSlot>();
                    //��ȯ�� ������ ����
                    ItemData _tempItem = _helmetSlot.item;
                    _helmetSlot.ClearSlot();
                    _helmetSlot.AddItem(item);
                    if (_tempItem != null)
                        AddItem(_tempItem);
                    else
                        ClearSlot();
                }
                else if (item.itemType == Constants.ItemType.Chest)
                {
                    ChestSlot _chestSlot = UIManager.Instance.GetPopup(nameof(EquipmentPopup)).GetComponentInChildren<ChestSlot>();
                    //��ȯ�� ������ ����
                    ItemData _tempItem = _chestSlot.item;
                    _chestSlot.ClearSlot();
                    _chestSlot.AddItem(item);
                    if (_tempItem != null)
                        AddItem(_tempItem);
                    else
                        ClearSlot();
                }
                else if (item.itemType == Constants.ItemType.Pants)
                {
                    PantsSlot _pantsSlot = UIManager.Instance.GetPopup(nameof(EquipmentPopup)).GetComponentInChildren<PantsSlot>();
                    //��ȯ�� ������ ����
                    ItemData _tempItem = _pantsSlot.item;
                    _pantsSlot.ClearSlot();
                    _pantsSlot.AddItem(item);
                    if (_tempItem != null)
                        AddItem(_tempItem);
                    else
                        ClearSlot();
                }
                else if (item.itemType == Constants.ItemType.Boots)
                {
                    BootsSlot _bootsSlot = UIManager.Instance.GetPopup(nameof(EquipmentPopup)).GetComponentInChildren<BootsSlot>();
                    //��ȯ�� ������ ����
                    ItemData _tempItem = _bootsSlot.item;
                    _bootsSlot.ClearSlot();
                    _bootsSlot.AddItem(item);
                    if (_tempItem != null)
                        AddItem(_tempItem);
                    else
                        ClearSlot();
                }
                else if (item.itemType == Constants.ItemType.Necklace)
                {
                    NecklaceSlot _necklaceSlot = UIManager.Instance.GetPopup(nameof(EquipmentPopup)).GetComponentInChildren<NecklaceSlot>();
                    //��ȯ�� ������ ����
                    ItemData _tempItem = _necklaceSlot.item;
                    _necklaceSlot.ClearSlot();
                    _necklaceSlot.AddItem(item);
                    if (_tempItem != null)
                        AddItem(_tempItem);
                    else
                        ClearSlot();
                }
                else if (item.itemType == Constants.ItemType.Ring)
                {
                    RingSlot _ringSlot = UIManager.Instance.GetPopup(nameof(EquipmentPopup)).GetComponentInChildren<RingSlot>();
                    //��ȯ�� ������ ����
                    ItemData _tempItem = _ringSlot.item;
                    _ringSlot.ClearSlot();
                    _ringSlot.AddItem(item);
                    if (_tempItem != null)
                        AddItem(_tempItem);
                    else
                        ClearSlot();
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            ItemDescription.instance.SetColor(item);
            ItemDescription.instance.gameObject.SetActive(true);
            ItemDescription.instance.transform.position = eventData.position;
            ItemDescription.instance.itemName.text = item.itemName;
            ItemDescription.instance.itemDescription.text = item.itemDescription;
            ItemDescription.instance.ShowStats(item);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDescription.instance.gameObject.SetActive(false);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            SetColor(0.5f);
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.dragItem = item;
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            SetColor(1);
        }
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragItem = null;
        DragSlot.instance.dragSlot = null;
        DragSlot.instance.equipmentSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null|| DragSlot.instance.equipmentSlot != null)
            ChangeSlot();
    }

    protected virtual void ChangeSlot()
    {
        ItemData _tempItem = item;
        int _tempItemCount = itemCount;
        if (DragSlot.instance.dragSlot == this)
        {
            return;  // ���� ���� �޼��� ����
        }
        if (DragSlot.instance.dragSlot != null)
        {
            if (item == DragSlot.instance.dragSlot.item && (item.itemType == Constants.ItemType.Material||item.itemType==Constants.ItemType.Consume))
            {
                if(itemCount + DragSlot.instance.dragSlot.itemCount > item.itemMax_Stack)
                {
                    SetSlotCount(DragSlot.instance.dragSlot.itemCount);
                    DragSlot.instance.dragSlot.SetSlotCount(_tempItemCount - item.itemMax_Stack);
                    return;
                }
                SetSlotCount(DragSlot.instance.dragSlot.itemCount);
                DragSlot.instance.dragSlot.ClearSlot();
                return;
            }
            AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
            if (_tempItem != null)
            {
                DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
            }
            else
                DragSlot.instance.dragSlot.ClearSlot();
        }
            
        else
        {
            
            if (_tempItem != null)
            {
                //������ Ÿ���� ���� ��쿡�� ��ȯ����
                if(DragSlot.instance.equipmentSlot.item.itemType == _tempItem.itemType)
                {
                    AddItem(DragSlot.instance.equipmentSlot.item);
                    DragSlot.instance.equipmentSlot.AddItem(_tempItem);
                }
                else
                {
                    //�����Ҽ�����
                    Debug.Log("�ٲܼ������ϴ�.");
                    return;
                }
            }
            else
            {
                AddItem(DragSlot.instance.equipmentSlot.item);
                DragSlot.instance.equipmentSlot.ClearSlot();
            }
        }
    }
}
