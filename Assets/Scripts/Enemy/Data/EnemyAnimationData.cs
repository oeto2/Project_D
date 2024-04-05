using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";
    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";
    [SerializeField] private string deadParameterName = "Dead";
    [SerializeField] private string stiffParameterName = "Stiff";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AirParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }
    public int DeadParameterHash { get; private set; }

    public int StiffParameterHash { get; private set; }
    
    //해쉬 값 할당
    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        AirParameterHash = Animator.StringToHash(airParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        DeadParameterHash = Animator.StringToHash(deadParameterName);
        StiffParameterHash = Animator.StringToHash(stiffParameterName);
    }
}
