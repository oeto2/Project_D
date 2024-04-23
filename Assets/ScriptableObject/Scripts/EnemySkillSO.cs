using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySkillData
{
    //��ų�̸�
    [SerializeField] public string SkillName = "ȸ������";
    //��ų���� �ð�
    [SerializeField] public float SkillDurationTime = 3f;
    //��ų ��Ÿ��
    [SerializeField] public float SkillCollTime = 15f;
    //��ų ���ݷ�
    [SerializeField] public int SkillDamage = 30;
    //��ų ��Ÿ�
    [SerializeField] public float SkillRange = 1.5f;
    //��ų ������ �ֱ� 
    [SerializeField] public float SkillDamageCycle = 0.5f;
}

[CreateAssetMenu(fileName = "EnemySkill", menuName = "Characters/Enemy/SkillSO")]
public class EnemySkillSO : ScriptableObject
{
    public List<EnemySkillData> skill_Data;
}
