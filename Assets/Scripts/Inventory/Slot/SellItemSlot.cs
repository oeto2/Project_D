using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null  || DragSlot.instance.equipmentSlot != null)
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
                if(_tempItemCount == 1)
                {
                    Inventory.instance.UpdateGold((int)(DragSlot.instance.dragSlot.item.itemPrice * 0.8f));
                    DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount-1);
                    //Debug.Log(InformationManager.Instance.saveLoadData.gold);

                }
                else
                {
                    UIManager.Instance.ShowPopup<InputPopup>();
                    var inputPopup = UIManager.Instance.GetPopup(nameof(InputPopup)).GetComponent<InputPopup>();
                    inputPopup.sellSlot = DragSlot.instance.dragSlot;
                }
            }
            else
                DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}
