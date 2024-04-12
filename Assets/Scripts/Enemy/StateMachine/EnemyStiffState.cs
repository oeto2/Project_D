using UnityEngine;

public class EnemyStiffState : EnemyBaseState
{
    //���� ���� ���� �ð�
    private float stiffTime = 0f;

    //���� ��� �ð�
    private float elapsedTime = 0f;

    //�÷��̾� ü��
    private float playerHp = 0f;

    public EnemyStiffState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        playerHp = stateMachine.Enemy.Health.health;
        //���� ���� �ð� �ʱ�ȭ
        stiffTime = stateMachine.Enemy.Data.monsterStiff;
        //Debug.Log($"���� ��������, ����{stiffTime}��");

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
