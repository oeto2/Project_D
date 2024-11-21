using UnityEngine.AI;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        NavMeshAgent navMeshAgent = stateMachine.Enemy.NavMeshAgent;
        if (navMeshAgent.isOnNavMesh)
            navMeshAgent?.Stop();

        isMove = false;
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        SetTriggerAnimation(stateMachine.Enemy.AnimationData.DeadParameterHash);

        stateMachine.Enemy.enemyInteration_Object?.SetActive(true);
    }
}
