using Constants;
using UnityEngine.EventSystems;
public class BootsSlot : EquipmentSlot
{
    protected override void Awake()
    {
        base.Awake();
        var index = InformationManager.Instance.saveLoadData.equipmentItems[ItemType.Boots];
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
