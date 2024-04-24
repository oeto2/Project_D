using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
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
