using UnityEngine;

public class EnemyStiffState : EnemyBaseState
{
    //���� ���� ���� �ð�
    private float stiffTime = 0f;

    //���� ��� �ð�
    private float elapsedTime = 0f;
    
    public EnemyStiffState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        //���� ���� �ð� �ʱ�ȭ
        stiffTime = stateMachine.Enemy.Data.monsterStiff;

        elapsedTime = 0f;
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.StiffParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.StiffParameterHash);
    }

    public override void Update()
    {
        //Debug.Log("����������");
        elapsedTime += Time.deltaTime;
        if (stiffTime < elapsedTime)
        {
            //Debug.Log("�������� ��");
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
