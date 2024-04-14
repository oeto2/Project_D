using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _monsterSpawnPoints;
    [SerializeField] private List<Transform> _EnemyPatrolLocations;

    //������ ���� ����Ʈ
    [SerializeField] private List<GameObject> _spawnMonsters;

    private void Start()
    {
        SpawnMonsters();
        SettingMonsters();
    }

    //���� ����
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

    //���� ����
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
