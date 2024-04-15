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

    private void Awake()
    {
        ClearSlot();
    }

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
        itemCount += count_;
        _textCount.text = itemCount.ToString();

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Constants.ItemType.Weapon)
                {
                    //���� ����
                }
                else if(item.itemType == Constants.ItemType.Equip)
                {
                    //�������
                }
                else
                {
                    GameManager.Instance.UsePotion(item);
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            ItemDescription.instance.gameObject.SetActive(true);
            ItemDescription.instance.transform.position = eventData.position;
            ItemDescription.instance.itemName.text = item.itemName;
            ItemDescription.instance.itemDescription.text = item.itemDescription;
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
        DragSlot.instance.weaponSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null || DragSlot.instance.weaponSlot != null || DragSlot.instance.equipmentSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        ItemData _tempItem = item;
        int _tempItemCount = itemCount;
        if (DragSlot.instance.dragSlot == this)
        {
            return;  // ���� ���� �޼��� ����
        }
        if (DragSlot.instance.dragSlot != null)
        {
            if(item == DragSlot.instance.dragSlot.item && (item.itemType == Constants.ItemType.Material||item.itemType==Constants.ItemType.Consume))
            {
                SetSlotCount(DragSlot.instance.dragSlot.itemCount);
                DragSlot.instance.dragSlot.ClearSlot();
                return;
            }
            AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
            if (_tempItem != null)
                DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
            else
                DragSlot.instance.dragSlot.ClearSlot();
        }
            
        else if (DragSlot.instance.equipmentSlot != null)
        {
            
            if (_tempItem != null)
                if (_tempItem.itemType == Constants.ItemType.Equip)
                {
                    DragSlot.instance.equipmentSlot.AddItem(_tempItem);
                    AddItem(DragSlot.instance.equipmentSlot.item);
                }
                else
                {
                    //�����Ҽ�����
                    Debug.Log("�ٲܼ������ϴ�.");
                    return;
                }
                    
            else
            {
                AddItem(DragSlot.instance.equipmentSlot.item);
                DragSlot.instance.equipmentSlot.ClearSlot();
            }
                
        }

        else
        {
            if (_tempItem != null)
                if (_tempItem.itemType == Constants.ItemType.Weapon)
                {
                    AddItem(DragSlot.instance.weaponSlot.item);
                    DragSlot.instance.weaponSlot.AddItem(_tempItem);
                } 
                else
                {
                    //�����Ҽ�����
                    Debug.Log("�ٲܼ������ϴ�.");
                    return;
                }
            else
            {
                AddItem(DragSlot.instance.weaponSlot.item);
                DragSlot.instance.weaponSlot.ClearSlot();
            }
                
        }
    }

}
