using Constants;
using UnityEngine.EventSystems;
public class BootsSlot : EquipmentSlot
{
    protected override void Awake()
    {
        base.Awake();
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Boots];
        // 인포매니저에서 데이터가 비어있으면 초기화, 아니면 집어넣기
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
        //if문에서 장비인지 확인
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
