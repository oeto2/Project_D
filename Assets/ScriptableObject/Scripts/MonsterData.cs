using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "New Monster")]
public class MonsterData : ScriptableObject
{
    [field: SerializeField] public string _name { get; private set; }
    [field: SerializeField] public int _level { get; private set; }
    [field: SerializeField] public float _hp { get; private set; }
    [field: SerializeField] public float _def { get; private set; }
    [field: SerializeField] public float _atk { get; private set; }
    [field: SerializeField] public float _atkSpeed { get; private set; }
    [field: SerializeField] public float _speed { get; private set; }
}
