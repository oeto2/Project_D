using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDB : MonoBehaviour
{
    private Dictionary<int, MonsterData> monsters = new();

    public MonsterDB()
    {
        var res = Resources.Load<MonsterDBSheet>("DB/MonsterDBSheet");
    }

}
