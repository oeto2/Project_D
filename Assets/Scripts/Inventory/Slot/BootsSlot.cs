using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BootsSlot : EquipmentSlot
{
    private void Start()
    {
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Boots];
        // �����Ŵ������� �����Ͱ� ��������� �ʱ�ȭ, �ƴϸ� ����ֱ�
        if (index == 0)
            ClearSlot();
        else
        {
            AddItem(Database.Item.Get(index));
        }

        EquipStats += _player.Stats.EquipItem;
        UnEquipStats += _player.Stats.UnEquipItem;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //if������ ������� Ȯ��
        if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Boots)
            ChangeSlot();
    }
    private void OnDisable()
    {
        if (item != null)
        {
            InformationManager.Instance.SaveInformation(ItemType.Boots, item.id);
        }
        else
            InformationManager.Instance.SaveInformation(ItemType.Boots, 0);
    }
}
