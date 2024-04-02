using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropPerData
{
    [SerializeField] private int _id;
    [SerializeField] private ItemGrade _rewardType1;
    [SerializeField] private float _dropPer1;
    [SerializeField] private ItemGrade _rewardType2;
    [SerializeField] private float _dropPer2;
    [SerializeField] private ItemGrade _rewardType3;
    [SerializeField] private float _dropPer3;
    [SerializeField] private ItemGrade _rewardType4;
    [SerializeField] private float _dropPer4;

    public int id => _id;
    public ItemGrade rewardType1 => _rewardType1;
    public float dropPer1 => _dropPer1;
    public ItemGrade rewardType2 => _rewardType2;
    public float dropPer2 => _dropPer2;
    public ItemGrade rewardType3 => _rewardType3;
    public float dropPer3 => _dropPer3;
    public ItemGrade rewardType4 => _rewardType4;
    public float dropPer4 => _dropPer4;
}
