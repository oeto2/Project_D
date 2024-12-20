using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";
    [SerializeField] private string defenseParameterName = "Defense";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";
    [SerializeField] private string baseSkillParameterName = "BaseSkill";

    [SerializeField] private string dieParameterName = "Die";


    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int DefenseParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int fallParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }
    public int BaseSkillParameterHash { get; private set; }

    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        DefenseParameterHash = Animator.StringToHash(defenseParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        fallParameterHash = Animator.StringToHash(fallParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
        BaseSkillParameterHash = Animator.StringToHash(baseSkillParameterName);

        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}