using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySkillData
{
    //스킬이름
    [SerializeField] public string SkillName = "회전베기";
    //스킬지속 시간
    [SerializeField] public float SkillDurationTime = 3f;
    //스킬 쿨타임
    [SerializeField] public float SkillCollTime = 15f;
    //스킬 공격력
    [SerializeField] public int SkillDamage = 30;
    //스킬 사거리
    [SerializeField] public float SkillRange = 1.5f;
    //스킬 데미지 주기 
    [SerializeField] public float SkillDamageCycle = 0.5f;
}

[CreateAssetMenu(fileName = "EnemySkill", menuName = "Characters/Enemy/SkillSO")]
public class EnemySkillSO : ScriptableObject
{
    public List<EnemySkillData> skill_Data;
}
