using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[System.Serializable]
public class ClassData
{
    [SerializeField] private int _id;
    [SerializeField] private ClassType _className;
    [SerializeField] private float _hp;
    [SerializeField] private float _mp;
    [SerializeField] private float _atk;
    [SerializeField] private float _def;
    [SerializeField] private float _spd;
    [SerializeField] private float _atkSpd;

    public int id => _id;
    public ClassType className => _className;
    public float hp => _hp;
    public float mp => _mp;
    public float atk => _atk;
    public float def => _def;
    public float spd => _spd;
    public float atkSpd => _atkSpd;
}
