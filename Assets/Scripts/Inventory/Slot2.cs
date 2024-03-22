using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public ItemData item; // ȹ���� ������.
    public int itemCount; // ȹ���� �������� ����.
    public Image itemImage; // �������� �̹���.


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
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // ������ ȹ��
    public void AddItem(ItemData _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
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
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
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
                    Debug.Log(item.itemName + " �� ����߽��ϴ�");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
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
        if (DragSlot.instance.dragSlot != null)
        {
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
