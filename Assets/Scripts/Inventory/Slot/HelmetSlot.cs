using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelmetSlot : EquipmentSlot
{
    private void Start()
    {
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Helmet];
        // 인포매니저에서 데이터가 비어있으면 초기화, 아니면 집어넣기
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
        //if문에서 장비인지 확인
        if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Helmet)
            ChangeSlot();
    }
    private void OnDisable()
    {
        if (item != null)
        {
            InformationManager.Instance.SaveInformation(ItemType.Helmet, item.id);
        }
        else
            InformationManager.Instance.SaveInformation(ItemType.Helmet, 0);
    }
}
