using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    // States
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }
    public PlayerComboAttackState ComboAttackState { get; }
    public PlayerDefenseState DefenseState { get; }
    public PlayerSkillState SkillState { get; }
    public PlayerDieState DieState { get; }

    // 
    public Vector2 MovementInput { get; set; }

    public Vector2 LookInput { get; set; }
    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }
    public bool IsDefensing { get; set; }
    public int ComboIndex { get; set; }
    public int SkillIndex { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        ComboAttackState = new PlayerComboAttackState(this);
        DefenseState = new PlayerDefenseState(this);
        SkillState = new PlayerSkillState(this);
        DieState = new PlayerDieState(this);
        MainCameraTransform = Camera.main.transform;
    }
}