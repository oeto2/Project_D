using Constants;
using UnityEngine;

public class SpawnInfomation : MonoBehaviour
{
    [SerializeField] private MonsterName _SpawnMonsterName;

    public string GetSpawnMonsterPath()
    {
        switch (_SpawnMonsterName)
        {
            case MonsterName.Skeleton:
                return "Monster/SKELETON";

            case MonsterName.Goblin:
                return "Monster/Goblin";

            case MonsterName.OrkWarrior:
                return "Monster/Boss/OrkWarrior";

            case MonsterName.Mimic:
                int rand = Random.Range(0, 2);
                if (rand == 0)
                    return "Monster/Mimic";
                else
                    return "Object/Chest";
        }

        return "Monster/SKELETON";
    }
}
