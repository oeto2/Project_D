using UnityEngine;

public class EnemyStiffState : EnemyBaseState
{
    //몬스터 경직 상태 시간
    private float stiffTime = 0f;

    //경직 경과 시간
    private float elapsedTime = 0f;

    public EnemyStiffState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        //경직 상태 시간 초기화
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
        Debug.Log("몬스터 경직상태");
        if (stiffTime < elapsedTime)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
