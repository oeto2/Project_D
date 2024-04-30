using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{

    public static DragSlot instance;

    public ItemData dragItem;

    public Slot dragSlot;

    public EquipmentSlot equipmentSlot;

    // 아이템 이미지.
    [SerializeField]
    private Image imageItem;

    void Awake()
    {
        instance = this;
    }

    public void DragSetImage(Image itemImage_)
    {
        imageItem.sprite = itemImage_.sprite;
        SetColor(alpha_: 1);
    }

    public void SetColor(float alpha_)
    {
        Color color = imageItem.color;
        color.a = alpha_;
        imageItem.color = color;
    }
}
