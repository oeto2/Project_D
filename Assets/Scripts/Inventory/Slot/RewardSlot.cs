using UnityEngine;
using UnityEngine.EventSystems;

public class RewardInfo
{
    //획득하려는 아이템의 ID값
    public int GetItemId;
    //획득하려는 아이템의 갯수
    public int GetItemCount;

    public RewardInfo(int getItemId_, int getItemCount_)
    {
        GetItemId = getItemId_;
        GetItemCount = getItemCount_;
    }
}

public class RewardSlot : Slot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Inventory inventory = UIManager.Instance.GetPopup(nameof(InventoryPopup)).GetComponent<Inventory>();
            if(inventory.AcquireItem(item, itemCount))
                ClearSlot();

            if (UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
    }
    protected override void ChangeSlot()
    {
        ItemData _tempItem = item;
        int _tempItemCount = itemCount;
        if (DragSlot.instance.dragSlot == this)
        {
            return;  // 변경 없이 메서드 종료
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

            if(UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
            
        else
        {
            
            if (_tempItem != null)
            {
                //아이템 타입이 같은 경우에만 교환가능
                if(DragSlot.instance.equipmentSlot.item.itemType == _tempItem.itemType)
                {
                    AddItem(DragSlot.instance.equipmentSlot.item);
                    DragSlot.instance.equipmentSlot.AddItem(_tempItem);
                }
                else
                {
                    //착용할수없음
                    Debug.Log("바꿀수없습니다.");
                    return;
                }
            }
            else
            {
                AddItem(DragSlot.instance.equipmentSlot.item);
                DragSlot.instance.equipmentSlot.ClearSlot();
            }

            if (UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
    }
}
