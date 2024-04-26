using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public static ItemDescription instance;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemStats;

    private Color _commonColor;
    private Color _uncommonColor;
    private Color _rareColor;
    private Color _uniqueColor;
    private Color _nomalColor;
    void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
        _nomalColor = Color.white;
        _commonColor = new Color(110f / 255f, 70f / 255f, 50f / 255f);
        _uncommonColor = new Color(138f / 255f, 133f / 255f, 129f / 255f);
        _rareColor = new Color(102f / 255f, 129f / 255f, 159f / 255f);
        _uniqueColor = new Color(215f / 255f, 175f / 255f, 98f / 255f);
    }
    public void SetColor(ItemData itemData_)
    {
        if (itemData_.itemType == Constants.ItemType.Material || itemData_.itemType == Constants.ItemType.Consume)
        {
            itemName.color = _nomalColor;
            itemDescription.color = _nomalColor;
            return;
        }
        if(itemData_.itemGrade == Constants.ItemGrade.Common)
        {
            itemName.color = _commonColor;
            itemDescription.color = _commonColor;
        }
        else if(itemData_.itemGrade == Constants.ItemGrade.Uncommon)
        {
            itemName.color = _uncommonColor;
            itemDescription.color = _uncommonColor;
        }
        else if (itemData_.itemGrade == Constants.ItemGrade.Rare)
        {
            itemName.color = _rareColor;
            itemDescription.color = _rareColor;
        }
        else
        {
            itemName.color = _uniqueColor;
            itemDescription.color = _uniqueColor;
        }
    }
    public void ShowStats(ItemData itemData_)
    {
        if(itemData_.itemType == Constants.ItemType.Consume)
            itemStats.text = "Recovery : " + (itemData_.itemHpRecover+itemData_.itemMpRecover).ToString();
        else if(itemData_.itemType == Constants.ItemType.Weapon)
            itemStats.text = "Atk : "+itemData_.itemAtk.ToString();
        else
            itemStats.text = "Def : " +itemData_.itemDef.ToString()+ "%";
    }
}
