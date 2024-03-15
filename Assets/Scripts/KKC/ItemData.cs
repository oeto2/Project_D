using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[System.Serializable]
public class ItemData
{
    [SerializeField] private int Id;
    [SerializeField] private string Item_Name;
    [SerializeField] private ItemType Item_Type;
    [SerializeField] private int Item_Price;
    [SerializeField] private float Item_Atk;
    [SerializeField] private float Item_Def;
    [SerializeField] private string Item_Description;
    [SerializeField] private int Item_Max_Stack;
    [SerializeField] private ItemGrade Item_Grade;
    [SerializeField] private string Item_Image;

    public int id => Id;
    public string item_Name => Item_Name;
    public ItemType item_Type => Item_Type;
    public int item_Price => Item_Price;
    public float item_Atk => Item_Atk;
    public float item_Def => Item_Def;
    public string item_Description => Item_Description;
    public int item_Max_Stack => Item_Max_Stack;
    public ItemGrade item_Grade => Item_Grade;
    public string item_Image => Item_Image;

    private Sprite sprite;
    public Sprite Sprite
    {
        get
        {
            Debug.Log("ÀÐÀ½");
            sprite = Resources.Load<Sprite>(item_Image);

            return sprite;
        }
    }
}
