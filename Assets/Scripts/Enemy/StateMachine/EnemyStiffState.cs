using UnityEngine;

public class EnemyStiffState : EnemyBaseState
{
    //���� ���� ���� �ð�
    private float stiffTime = 0f;

    //���� ��� �ð�
    private float elapsedTime = 0f;

    public EnemyStiffState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        //���� ���� �ð� �ʱ�ȭ
        stiffTime = stateMachine.Enemy.Data.monsterStiff;
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
        Debug.Log("���� ��������");
        if (stiffTime < elapsedTime)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
