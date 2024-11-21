using UnityEngine;
using Constants;

public class EnemyIdleState : EnemyBaseState
{
    //현재 대기상태인지
    private bool _isWaiting;

    //현재 시간 값
    private float _elapsedTime;

    //설정된 시간
    private float _waitTime = 3f;

    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        //기다리는 시간 설정값 초기화
        _isWaiting = true;
        _elapsedTime = 0f;

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
        _elapsedTime += Time.deltaTime;

        //waitTime 
        if (_elapsedTime >= _waitTime)
        {
            _isWaiting = false;
        }

        //일정 범위내로 들어왔으면 플레이어 추적 하기
        if (IsInChaseRange() && !stateMachine.Enemy._targetHealth.IsDead)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        //정찰 타입이면 일정 구역 순찰하기
        if (!_isWaiting && stateMachine.Enemy.MonsterMoveType == MonsterMoveType.Scout)
        {
            stateMachine.ChangeState(stateMachine.WanderingState);
            return;
        }
    }
}



