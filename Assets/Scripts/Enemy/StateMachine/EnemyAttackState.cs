using Constants;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Enemy.transform.LookAt(stateMachine.Enemy.Target.transform.position);

        isMove = false;
        NavMeshAgent navMeshAgent = stateMachine.Enemy.NavMeshAgent;

        if (navMeshAgent.isOnNavMesh)
            navMeshAgent.SetDestination(stateMachine.Enemy.transform.position);

        stateMachine.MovementSpeedModifier = 0;
        base.Enter();

        switch (stateMachine.Enemy.MonsterType)
        {
            case MonsterType.Normal:
                StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
                StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
                break;

            case MonsterType.Boss:
                EnemySkillBase bossSkill = stateMachine.Enemy.EnemySkill;

                StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);

                //��ų�� ��밡���ϴٸ�
                if (bossSkill.Skill01Ready)
                {
                    //��ų ���
                    stateMachine.MovementSpeedModifier = 0.5f;
                    bossSkill.UseSkill(1);
                    StartAnimation(stateMachine.Enemy.AnimationData.Skill01ParameterHash);
                }

                else if (bossSkill.Skill02Ready)
                {
                    //��ų ���
                    bossSkill.UseSkill(2);
                    StartAnimation(stateMachine.Enemy.AnimationData.Skill02ParameterHash);
                }

                //��ų ����� �Ұ����ϴٸ�
                else
                {
                    //�⺻����
                    StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
                }
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.Skill01ParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.Skill02ParameterHash);
    }

    public override void Update()
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
    }
}