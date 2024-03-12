using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemDBSheet : ScriptableObject
{
	public List<ItemData> Item_Table;// replace 'entitytype' to an actual type that is serializable.
}
