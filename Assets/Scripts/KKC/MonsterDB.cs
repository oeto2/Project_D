using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDB
{
    private Dictionary<int, MonsterData> monsters = new();
    private Dictionary<int, DropPerData> dropPer = new();

    public MonsterDB()
    {
        var res = Resources.Load<MonsterDBSheet>("DB/MonsterDBSheet");
        var monsterSO = Object.Instantiate(res);
        var entities = monsterSO.Monster_Table;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var monster = entities[i];

            if (monsters.ContainsKey(monster.id))
                monsters[monster.id] = monster;
            else
                monsters.Add(monster.id, monster);
        }
    }

    public MonsterData Get(int id)
    {
        if (monsters.ContainsKey(id))
            return monsters[id];
        else
            return null;
    }
}
