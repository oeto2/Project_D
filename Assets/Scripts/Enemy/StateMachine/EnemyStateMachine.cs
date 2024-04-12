using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    //States
    public EnemyIdleState IdlingState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyWanderingState WanderingState { get; }
    public EnemyDeadState DeadState { get; }
    public EnemyStiffState StiffState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        IdlingState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        WanderingState = new EnemyWanderingState(this);
        DeadState = new EnemyDeadState(this);
        StiffState = new EnemyStiffState(this);

        MovementSpeed = enemy.Data.monsterRun;
        RotationDamping = enemy.Data.monsterRotationDamping;
    }
}
