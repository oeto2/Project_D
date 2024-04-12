using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "DropPerSheet")]
public class DropPerSheet : ScriptableObject
{
	public List<DropPerData> Drop_Table; // Replace 'EntityType' to an actual type that is serializable.
}
