using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestSlot : EquipmentSlot
{
    protected override void Awake()
    {
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Chest];
        // �����Ŵ������� �����Ͱ� ��������� �ʱ�ȭ, �ƴϸ� ����ֱ�
        if (index == 0)
            ClearSlot();
        else
        {
            AddItem(Database.Item.Get(index));
        }
        base.Awake();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //if������ ������� Ȯ��
        if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Chest)
            ChangeSlot();
    }
    private void OnDisable()
    {
        if (item != null)
        {
            InformationManager.Instance.SaveInformation(ItemType.Chest, item.id);
        }
        else
            InformationManager.Instance.SaveInformation(ItemType.Chest, 0);
    }
}
