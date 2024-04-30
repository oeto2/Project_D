using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isMove = true;
        NavMeshAgent navMeshAgent = stateMachine.Enemy.NavMeshAgent;
        navMeshAgent.speed = stateMachine.Enemy.stateMachine.MovementSpeed;

        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Enemy._targetHealth.IsDead)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
            return;
        }

        if (IsInAttackRange())
        {
            Debug.Log("공격범위 안에 들어옴");
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        
        else
        {
            if(!IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
            }
        }
    }

    //공격 범위
    private bool IsInAttackRange()
    {
        float monsterAtkRng = stateMachine.Enemy.Data.monsterAtkRng;
        float playerDistanceSqr = (stateMachine.Enemy.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= monsterAtkRng * monsterAtkRng;
    }
}
