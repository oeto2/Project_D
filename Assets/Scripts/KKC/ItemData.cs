using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[System.Serializable]
public class ItemData
{
    [SerializeField] private int _id;
    [SerializeField] private string _itemName;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _itemPrice;
    [SerializeField] private float _itemAtk;
    [SerializeField] private float _itemDef;
    [SerializeField] private string _itemDescription;
    [SerializeField] private int _itemMaxStack;
    [SerializeField] private ItemGrade _itemGrade;
    [SerializeField] private string _itemSprite;

    public int id => _id;
    public string itemName => _itemName;
    public ItemType itemType => _itemType;
    public int itemPrice => _itemPrice;
    public float itemAtk => _itemAtk;
    public float itemDef => _itemDef;
    public string itemDescription => _itemDescription;
    public int itemMax_Stack => _itemMaxStack;
    public ItemGrade itemGrade => _itemGrade;
    public string itemSprite => _itemSprite;

    private Sprite sprite;
    public Sprite Sprite
    {
        get
        {
            Debug.Log("ÀÐÀ½");
            sprite = Resources.Load<Sprite>(itemSprite);

            return sprite;
        }
    }
}
