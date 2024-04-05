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

    //플레이어 공격
    public void StartAttack()
    {
        Ray ray = new Ray(transform.position, (_playerObject.position - transform.position).normalized);
        RaycastHit hitData;

        //레이캐스트 사용
        Physics.Raycast(ray, out hitData, _enemy.Data.monsterAtkRng);

        if(hitData.transform?.tag == "Player")
        {
            hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage((int)_enemy.Data.monsterAtk);
        }
    }
}
