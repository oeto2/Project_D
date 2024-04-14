using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _monsterSpawnPoints;
    [SerializeField] private List<Transform> _EnemyPatrolLocations;

    //생성된 몬스터 리스트
    [SerializeField] private List<GameObject> _spawnMonsters;

    private void Start()
    {
        SpawnMonsters();
        SettingMonsters();
    }

    //몬스터 생성
    private void SpawnMonsters()
    {
        for (int i = 0; i < _monsterSpawnPoints.Count; i++)
        {
            SpawnInfomation spawnInfomation = _monsterSpawnPoints[i].GetComponent<SpawnInfomation>();
            _spawnMonsters.Add(ResourceManager.Instance.Instantiate(spawnInfomation.GetSpawnMonsterPath(), _monsterSpawnPoints[i]));
            Enemy enemy = _spawnMonsters[i].GetComponent<Enemy>();

            if (enemy != null)
                enemy.EnemyPatrolLocation_number = i;
        }
    }

    //몬스터 세팅
    private void SettingMonsters()
    {
        int locationNum = 0;
        for (int i = 0; i < _spawnMonsters.Count; i++)
        {
            Enemy enemy = _spawnMonsters[i].GetComponent<Enemy>();

            if (enemy == null)
                continue;
            if (enemy.MonsterMoveType == Constants.MonsterMoveType.Lock )
                continue;

            enemy.MonsterWanderDestination.Add(_EnemyPatrolLocations[locationNum++]);
            enemy.MonsterWanderDestination.Add(_EnemyPatrolLocations[locationNum++]);
        }
    }
}
