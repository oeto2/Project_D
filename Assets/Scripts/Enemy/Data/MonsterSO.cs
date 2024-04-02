using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSO : ScriptableObject
{
    public int id;
    public string monsterName;
    public float monsterHp;
    public float monsterAtk;
    public float monsterAtkSpd;
    public float monsterAtkRng;
    public float monsterDef;
    public float monsterWalk;
    public float monsterRun;
    public float monsterStiff;
    public int monsterMaxRoot;
    public int dropId;
    public int monsterChasingRng;
    public int monsterRotationDamping;
}
