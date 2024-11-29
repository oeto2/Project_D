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
        //리워드 아이템 우클릭 시
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Inventory inventory = UIManager.Instance.GetPopup(nameof(InventoryPopup)).GetComponent<Inventory>();
            
            //획득하려는 아이템이 존재하면 해당 슬롯 비우기 
             if(inventory.AcquireItem(item, itemCount))
                 ClearSlot();

             //리워드 팝업이 존재한다면
             if (UIManager.Instance.ExistPopup(nameof(RewardPopup)))
             {
                 GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                 GameManager.Instance.CallUpdateRewardCountEvent();
             }
            
            Debug.Log("리워드 아이템 획득");
        }
    }
    
    
    //아이템 위치 변경 : 인벤토리 -> 리워드 팝업
    protected override void ChangeSlot()
    {
        ItemData _tempItem = item;
        int _tempItemCount = itemCount;
        if (DragSlot.instance.dragSlot == this)
        {
            return;  // 변경 없이 메서드 종료
        }
        
        //아이템 획득
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

            //리워드 팝업이 존재한다면, 아이템 획득 이벤트 호출
            if(UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                //이벤트에 등록되어있는 아이템 리스트 획득
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                //모두 획득 관련 : 몬스터의 남은 아이템 갯수 업데이트
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
         
        //아이템 교체
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
