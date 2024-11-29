using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //����Ʈ : ���� ���� ��ġ 
    [SerializeField] private List<Transform> _monsterSpawnPoints;
    
    //����Ʈ : ���� ���� ��ġ
    [SerializeField] private List<Transform> _EnemyPatrolLocations;

    //������ ���� ����Ʈ
    [SerializeField] private List<GameObject> _spawnMonsters;

    //���� : ���� 1ȸ ������ ���� �÷���
    private bool _isWork = false;

    //�ݶ��̴� �浹 ��
    private void OnTriggerEnter(Collider other)
    {
        //�÷��̾� �浹 ��
        if (other.gameObject.CompareTag("Player") && !_isWork)
        {
            SpawnMonsters(); // ���� ��ȯ
            SettingMonsters(); // ���� ����
            _isWork = true; // �ߺ� ���� ����
            Debug.Log("�÷��̾� �浹��");
        }
    }

    //���� ����
    private void SpawnMonsters()
    {
        for (int i = 0; i < _monsterSpawnPoints.Count; i++)
        {
            //���� ������ �޾ƿͼ�, �ش� ������ ���͸� ��ȯ��
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
            if (enemy.MonsterMoveType == Constants.MonsterMoveType.Lock)
                continue;

            enemy.MonsterWanderDestination.Add(_EnemyPatrolLocations[locationNum++]);
            enemy.MonsterWanderDestination.Add(_EnemyPatrolLocations[locationNum++]);
        }
    }
}
