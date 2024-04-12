using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SkillInfoData
{
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public int SkillStateIndex { get; private set; }
    [field: SerializeField] public float ManaCost { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float SkillRange { get; private set; }
    [field: SerializeField] public float CoolDown { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
}

[Serializable]
public class PlayerSkillData
{
    [field: SerializeField] public List<SkillInfoData> SkillInfoDatas { get; private set; }
    public int GetSkillInfoCount() {  return SkillInfoDatas.Count; }
    public SkillInfoData GetSkillInfo(int index) { return SkillInfoDatas[index - 1]; }
}
