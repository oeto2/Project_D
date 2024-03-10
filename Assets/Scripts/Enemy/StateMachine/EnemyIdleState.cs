using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using Unity.VisualScripting;

public class EnemyIdleState : EnemyBaseState
{
    //현재 대기상태인지
    private bool isWaiting;

    //현재 시간 값
    private float elapsedTime;

    //설정된 시간
    private float WaitTime = 3f;

    public EnemyIdleState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        //기다리는 시간 설정값 초기화
        isWaiting = true;
        elapsedTime = 0f;

        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        elapsedTime += Time.deltaTime;

        //설정된 시간이 지났다면,
        if (elapsedTime >= WaitTime)
        {
            isWaiting = false;
        }

        //플레이어 추적 상태
        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        else
        {
            //정찰 타입이면 일정 구역 순찰하기
            if (!isWaiting && stateMachine.Enemy.MonsterMoveType == MonsterMoveType.Scout)
            {
                stateMachine.ChangeState(stateMachine.WanderingState);
                return;
            }
        }
    }
}
