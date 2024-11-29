using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //리스트 : 몬스터 스폰 위치 
    [SerializeField] private List<Transform> _monsterSpawnPoints;
    
    //리스트 : 몬스터 정찰 위치
    [SerializeField] private List<Transform> _EnemyPatrolLocations;

    //생성된 몬스터 리스트
    [SerializeField] private List<GameObject> _spawnMonsters;

    //변수 : 최초 1회 실행을 위한 플래그
    private bool _isWork = false;

    //콜라이더 충돌 시
    private void OnTriggerEnter(Collider other)
    {
        //플레이어 충돌 시
        if (other.gameObject.CompareTag("Player") && !_isWork)
        {
            SpawnMonsters(); // 몬스터 소환
            SettingMonsters(); // 몬스터 세팅
            _isWork = true; // 중복 실행 방지
            Debug.Log("플레이어 충돌함");
        }
    }

    //몬스터 생성
    private void SpawnMonsters()
    {
        for (int i = 0; i < _monsterSpawnPoints.Count; i++)
        {
            //스폰 정보를 받아와서, 해당 종류의 몬스터를 소환함
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
            if (enemy.MonsterMoveType == Constants.MonsterMoveType.Lock)
                continue;

            enemy.MonsterWanderDestination.Add(_EnemyPatrolLocations[locationNum++]);
            enemy.MonsterWanderDestination.Add(_EnemyPatrolLocations[locationNum++]);
        }
    }
}
