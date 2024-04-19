using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface EnemySkillable
{
    public void UseSkill(int skillNum_);
}


public class EnemySkillBase : MonoBehaviour, EnemySkillable
{
    //외부 컴포넌트
    protected NavMeshAgent _navMeshAgent;
    protected Enemy _enemy;

    [field: Header("SkillState")]
    [SerializeField] protected EnemySkillSO _skillData;

    //스킬들이 준비 되었는지
    [field: SerializeField] public bool Skill01Ready { get; set; } = true;
    [field: SerializeField] public bool Skill02Ready { get; set; } = true;

    //현재 스킬을 사용중인지
    [field: SerializeField] public bool UsingSkill { get; set; } = false;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();
    }

    public virtual void UseSkill(int skillNum_)
    {
    }
}
