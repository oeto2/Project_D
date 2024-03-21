using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public Transform Target { get; private set; }

    //States
    public EnemyIdleState IdlingState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyWanderingState WanderingState { get; }
    public EnemyDeadState DeadState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;

        //나중에는 수정하기
        Target = GameManager.Instance.playerObject.transform;

        IdlingState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        WanderingState = new EnemyWanderingState(this);
        DeadState = new EnemyDeadState(this);

        MovementSpeed = enemy.Data.GroundedData.BaseSpeed;
        RotationDamping = enemy.Data.GroundedData.BaseRotationDamping;
    }
}
