using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData item; // ȹ���� ������.
    public Image itemImage; // �������� �̹���.

    protected Player _player;

    protected event Action<ItemData> EquipStats;
    protected event Action<ItemData> UnEquipStats;

    protected virtual void Awake()
    {
        _player = GameManager.Instance.player;
        _player.Stats.OnDie += ClearSlot;
    }

    // �̹����� ���� ����.
    protected void SetColor(float alpha_)
    {
        Color color = itemImage.color;
        color.a = alpha_;
        itemImage.color = color;
    }

    // ������ ȹ��
    public void AddItem(ItemData item_)
    {
        item = item_;
        itemImage.sprite = item.Sprite;
        if (EquipStats != null)
            EquipStats(item);
        SetColor(1);
    }

    // ���� �ʱ�ȭ.
    public void ClearSlot()
    {
        if (UnEquipStats != null)
            UnEquipStats(item);
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                //�����������
                bool invenEmpty = UIManager.Instance.GetPopup(nameof(InventoryPopup)).GetComponent<Inventory>().AcquireItem(item);
                if(invenEmpty)
                    ClearSlot();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            ItemDescription.instance.SetColor(item);
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
            DragSlot.instance.equipmentSlot = this;
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
        if (item != null)
        {
            SetColor(1);
        }
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragItem = null;
        DragSlot.instance.dragSlot = null;
        DragSlot.instance.equipmentSlot = null;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        ////if������ ������� Ȯ��
        //if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Equip)
        //    ChangeSlot();
    }

    protected void ChangeSlot()
    {
        ItemData _tempItem = item;
        ClearSlot();
        AddItem(DragSlot.instance.dragSlot.item);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}
