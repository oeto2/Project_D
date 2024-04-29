using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;
    private Transform _playerObject;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _playerObject = GameManager.Instance.playerObject.transform;
    }

    //�÷��̾� ����
    public void StartAttack()
    {
        //Ray ray = new Ray(transform.position + Vector3.up, (_playerObject.position - transform.position).normalized);
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.forward * _enemy.Data.monsterAtkRng);
        RaycastHit hitData;

        //����ĳ��Ʈ ���
        Physics.Raycast(ray, out hitData, _enemy.Data.monsterAtkRng);

        if(hitData.transform?.tag == "Player")
        {
            hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage((int)_enemy.Data.monsterAtk);
        }
    }
}
