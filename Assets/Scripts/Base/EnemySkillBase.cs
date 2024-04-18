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
    //�ܺ� ������Ʈ
    protected NavMeshAgent _navMeshAgent;
    protected Enemy _enemy;

    [field: Header("SkillState")]
    [SerializeField] protected EnemySkillSO _skillData;

    //��ų���� �غ� �Ǿ�����
    [field: SerializeField] public bool Skill01Ready { get; set; } = true;
    [field: SerializeField] public bool Skill02Ready { get; set; } = true;

    //���� ��ų�� ���������
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
