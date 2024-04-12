using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropPerData
{
    [SerializeField] private int _id;
    [SerializeField] private ItemGrade _rewardType1;
    [SerializeField] private int _dropPer1;
    [SerializeField] private ItemGrade _rewardType2;
    [SerializeField] private int _dropPer2;
    [SerializeField] private ItemGrade _rewardType3;
    [SerializeField] private int _dropPer3;
    [SerializeField] private ItemGrade _rewardType4;
    [SerializeField] private int _dropPer4;
    [SerializeField] private int _totalDropPer;

    public int id => _id;
    public ItemGrade rewardType1 => _rewardType1;
    public int dropPer1 => _dropPer1;
    public ItemGrade rewardType2 => _rewardType2;
    public int dropPer2 => _dropPer2;
    public ItemGrade rewardType3 => _rewardType3;
    public int dropPer3 => _dropPer3;
    public ItemGrade rewardType4 => _rewardType4;
    public int dropPer4 => _dropPer4;
    public int totalDropPer => _totalDropPer;
}
