using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    //�÷��̾� ����
    public void StartAttack()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward * _enemy.Data.monsterAtkRng);
        //Debug.Log(ray.direction);
        RaycastHit hitData;

        //����ĳ��Ʈ ���
        Physics.Raycast(ray, out hitData, _enemy.Data.monsterAtkRng);

        if(hitData.transform?.tag == "Player")
        {
            hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage((int)_enemy.Data.monsterAtk);
        }
    }
}
