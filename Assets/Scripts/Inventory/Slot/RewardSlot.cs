using UnityEngine;
using UnityEngine.EventSystems;

public class RewardInfo
{
    //ȹ���Ϸ��� �������� ID��
    public int GetItemId;
    //ȹ���Ϸ��� �������� ����
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
        //������ ������ ��Ŭ�� ��
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Inventory inventory = UIManager.Instance.GetPopup(nameof(InventoryPopup)).GetComponent<Inventory>();
            
            //ȹ���Ϸ��� �������� �����ϸ� �ش� ���� ���� 
             if(inventory.AcquireItem(item, itemCount))
                 ClearSlot();

             //������ �˾��� �����Ѵٸ�
             if (UIManager.Instance.ExistPopup(nameof(RewardPopup)))
             {
                 GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                 GameManager.Instance.CallUpdateRewardCountEvent();
             }
            
            Debug.Log("������ ������ ȹ��");
        }
    }
    
    
    //������ ��ġ ���� : �κ��丮 -> ������ �˾�
    protected override void ChangeSlot()
    {
        ItemData _tempItem = item;
        int _tempItemCount = itemCount;
        if (DragSlot.instance.dragSlot == this)
        {
            return;  // ���� ���� �޼��� ����
        }
        
        //������ ȹ��
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

            //������ �˾��� �����Ѵٸ�, ������ ȹ�� �̺�Ʈ ȣ��
            if(UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                //�̺�Ʈ�� ��ϵǾ��ִ� ������ ����Ʈ ȹ��
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                //��� ȹ�� ���� : ������ ���� ������ ���� ������Ʈ
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
         
        //������ ��ü
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

            if (UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
    }
}
