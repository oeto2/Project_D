using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "MonsterDBSheet")]
public class MonsterDBSheet : ScriptableObject
{
	public List<MonsterData> Monster_Table; // replace 'entitytype' to an actual type that is serializable.
}
