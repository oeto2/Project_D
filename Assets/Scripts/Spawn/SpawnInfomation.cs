using Constants;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnInfomation : MonoBehaviour
{
    [SerializeField] private MonsterName _SpawnMonsterName;

    public string GetSpawnMonsterPath()
    {
        switch(_SpawnMonsterName)
        {
            case MonsterName.Skeleton:
                return "Monster/SKELETON";

            case MonsterName.Goblin:
                return "Monster/Goblin";

            case MonsterName.OrkWarrior:
                return "Monster/Boss/OrkWarrior";
        }

        return "Monster/SKELETON";
    }
}
