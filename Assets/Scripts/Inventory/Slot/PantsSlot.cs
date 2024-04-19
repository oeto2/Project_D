using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PantsSlot : EquipmentSlot
{
    protected override void Awake()
    {
        base.Awake();
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Pants];
        // �����Ŵ������� �����Ͱ� ��������� �ʱ�ȭ, �ƴϸ� ����ֱ�
        if (index == 0)
            ClearSlot();
        else
        {
            AddItem(Database.Item.Get(index));
        }
    }
    private void Start()
    {
        EquipStats += _player.Stats.EquipItem;
        UnEquipStats += _player.Stats.UnEquipItem;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //if������ ������� Ȯ��
        if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Pants)
            ChangeSlot();
    }

    private void OnDisable()
    {
        if (item != null)
        {
            InformationManager.Instance.SaveInformation(ItemType.Pants, item.id);
        }
        else
            InformationManager.Instance.SaveInformation(ItemType.Pants, 0);
    }
}
