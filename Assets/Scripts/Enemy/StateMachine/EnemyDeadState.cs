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
        isMove = false;
        stateMachine.MovementSpeedModifier = 0f;
        stateMachine.Enemy.NavMeshAgent.Stop();

        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        SetTriggerAnimation(stateMachine.Enemy.AnimationData.DeadParameterHash);
    }
}
