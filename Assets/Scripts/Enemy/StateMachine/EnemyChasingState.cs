using UnityEngine.AI;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }
    
    public override void Enter()
    {
        NavMeshAgent navMeshAgent = stateMachine.Enemy.NavMeshAgent;
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = stateMachine.Enemy.stateMachine.MovementSpeed;
        }

        //Debug.Log("추적상태 진입");
        stateMachine.MovementSpeedModifier = 1;

        base.Enter();
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
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

        else if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }

    //공격 범위
    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Enemy.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enemy.Data.monsterAtkRng* stateMachine.Enemy.Data.monsterAtkRng;
    }
}
