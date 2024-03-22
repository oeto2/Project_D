using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItems", menuName = "ShopList")]
public class ShopListSO : ScriptableObject
{
    public List<int> ShopItems = new List<int>();
}
