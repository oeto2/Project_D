using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "ClassDBSheet")]
public class ClassDBSheet : ScriptableObject
{
	public List<ClassData> Class_Table; // Replace 'EntityType' to an actual type that is serializable.
}
