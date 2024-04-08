using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "ShopDBSheet")]
public class ShopDBSheet : ScriptableObject
{
	public List<ShopData> Shop_Table; // replace 'entitytype' to an actual type that is serializable.
}
