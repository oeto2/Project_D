using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[System.Serializable]
public class MonsterData
{
    [SerializeField] private int _id;
    [SerializeField] private string _monsterName;
    [SerializeField] private float _monsterHp;
    [SerializeField] private float _monsterAtk;
    [SerializeField] private float _monsterAtkSpd;
    [SerializeField] private float _monsterAtkRng;
    [SerializeField] private float _monsterDef;
    [SerializeField] private float _monsterWalk;
    [SerializeField] private float _monsterRun;
    [SerializeField] private float _monsterStiff;
    [SerializeField] private int _monsterMaxRoot;
    [SerializeField] private int _dropId;

    public int id => _id;
    public string monsterName => _monsterName;
    public float monsterHp => _monsterHp;
    public float monsterAtk => _monsterAtk;
    public float monsterAtkSpd => _monsterAtkSpd;
    public float monsterAtkRng => _monsterAtkRng;
    public float monsterDef => _monsterDef;
    public float monsterWalk => _monsterWalk;
    public float monsterRun => _monsterRun;
    public float monsterStiff => _monsterStiff;
    public int monsterMaxRoot => _monsterMaxRoot;
    public int dropId => _dropId;
}
