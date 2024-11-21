using UnityEngine;
using Constants;

public class EnemyIdleState : EnemyBaseState
{
    //���� ����������
    private bool _isWaiting;

    //���� �ð� ��
    private float _elapsedTime;

    //������ �ð�
    private float _waitTime = 3f;

    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        //��ٸ��� �ð� ������ �ʱ�ȭ
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

        //���� �������� �������� �÷��̾� ���� �ϱ�
        if (IsInChaseRange() && !stateMachine.Enemy._targetHealth.IsDead)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        //���� Ÿ���̸� ���� ���� �����ϱ�
        if (!_isWaiting && stateMachine.Enemy.MonsterMoveType == MonsterMoveType.Scout)
        {
            stateMachine.ChangeState(stateMachine.WanderingState);
            return;
        }
    }
}



