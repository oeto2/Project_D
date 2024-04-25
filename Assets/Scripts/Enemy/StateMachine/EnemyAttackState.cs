using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyAppliedForce;

    //해당 오브젝트가 보스인지
    private bool isBoss;

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();

        switch (stateMachine.Enemy.MonsterType)
        {
            case MonsterType.Normal:
                StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
                StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
                break;

            case MonsterType.Boss:
                isBoss = true;
                EnemySkillBase bossSkill = stateMachine.Enemy.EnemySkill;

                StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);

                //스킬이 사용가능하다면
                if(bossSkill.Skill01Ready)
                {
                    //스킬 사용
                    stateMachine.MovementSpeedModifier = 0.5f;
                    bossSkill.UseSkill(1);
                    StartAnimation(stateMachine.Enemy.AnimationData.Skill01ParameterHash);
                }

                else if (bossSkill.Skill02Ready)
                {
                    //스킬 사용
                    bossSkill.UseSkill(2);
                    StartAnimation(stateMachine.Enemy.AnimationData.Skill02ParameterHash);
                }

                //스킬 사용이 불가능하다면
                else
                {
                    //기본공격
                    StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
                }
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.Skill01ParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.Skill02ParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
        //else
        //{
        //    if (IsInChaseRange())
        //    {
                
        //        return;
        //    }
        //    else
        //    {
        //        stateMachine.ChangeState(stateMachine.IdlingState);
        //        return;
        //    }
        //}
    }
}