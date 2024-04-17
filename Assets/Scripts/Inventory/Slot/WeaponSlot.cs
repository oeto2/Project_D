using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : EquipmentSlot
{
    private void Awake()
    {
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Weapon];
        // 인포매니저에서 데이터가 비어있으면 초기화, 아니면 집어넣기
        if (index == 0)
            ClearSlot();
        else
        {
            AddItem(Database.Item.Get(index));
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //if문에서 무슨장비인지 확인
        if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Weapon)
            ChangeSlot();
    }
    private void OnDisable()
    {
        if (item != null)
        {
            InformationManager.Instance.SaveInformation(ItemType.Weapon, item.id);
        }
        else
            InformationManager.Instance.SaveInformation(ItemType.Weapon, 0);
    }
}
