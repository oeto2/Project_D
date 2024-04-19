using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NecklaceSlot : EquipmentSlot
{
    protected override void Awake()
    {
        base.Awake();
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Necklace];
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
        if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Necklace)
            ChangeSlot();
    }
    private void OnDisable()
    {
        if (item != null)
        {
            InformationManager.Instance.SaveInformation(ItemType.Necklace, item.id);
        }
        else
            InformationManager.Instance.SaveInformation(ItemType.Necklace, 0);
    }
}
