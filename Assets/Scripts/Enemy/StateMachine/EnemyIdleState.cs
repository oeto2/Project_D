using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using Unity.VisualScripting;

public class EnemyIdleState : EnemyBaseState
{
    //���� ����������
    private bool isWaiting;

    //���� �ð� ��
    private float elapsedTime;

    //������ �ð�
    private float WaitTime = 3f;

    public EnemyIdleState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("�⺻ ���� ����");

        //��ٸ��� �ð� ������ �ʱ�ȭ
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

        //������ �ð��� �����ٸ�,
        if (elapsedTime >= WaitTime)
        {
            isWaiting = false;
        }

        //���� �������� �������� �÷��̾� ���� �ϱ�
        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }

        else
        {
            //���� Ÿ���̸� ���� ���� �����ϱ�
            if (!isWaiting && stateMachine.Enemy.MonsterMoveType == MonsterMoveType.Scout)
            {
                stateMachine.ChangeState(stateMachine.WanderingState);
                return;
            }
        }
    }
}
