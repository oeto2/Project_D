using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null || DragSlot.instance.weaponSlot != null || DragSlot.instance.equipmentSlot != null)
            SellItem();
    }

    private void SellItem()
    {
        ItemData _tempItem = DragSlot.instance.dragSlot.item;
        int _tempItemCount = DragSlot.instance.dragSlot.itemCount;
        if (DragSlot.instance.dragSlot != null)
        {
            if (_tempItem != null)
            {
                InformationManager.Instance.gold += (int)(DragSlot.instance.dragSlot.item.itemPrice*0.8f);
                DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount-1);
                Debug.Log(InformationManager.Instance.gold);
            }
            else
                DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}
