using DarkPixelRPGUI.Scripts.UI.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot Instance;

    public Slot2 dragSlot;
    [SerializeField]
    private Image _itemImage;

    public void DragSetImage(Image itemImage)
    {
        _itemImage.sprite = itemImage.sprite;

    }
    public void VisibleImage()
    {

    }
}
