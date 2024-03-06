using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterData monsterData;

    private void Awake()
    {
        monsterData = ResourceManager.Instance.Load<MonsterData>("SO/Skeleton");
    }
}
