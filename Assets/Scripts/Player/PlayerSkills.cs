using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    SkillInfoData skillInfoData;

    [SerializeField] private List<GameObject> _skillEffect;
    public float[] coolDowns;
    private ParticleSystem _ps;
    //public List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private GameObject _playerObj;
    private Player _player;

    public event Action<int> coolDownUI;

    private void Awake()
    {
        _player = _playerObj.GetComponent<Player>();
        coolDowns = new float[_player.Data.SkillData.GetSkillInfoCount()];
    }

    private void Update()
    {
        for (int i = 0; i < coolDowns.Length; i++)
        {
            if (coolDowns[i] >= 0)
            {
                coolDowns[i] = MathF.Max(coolDowns[i] - Time.deltaTime, 0);
                if (coolDownUI != null)
                    coolDownUI(i);
            }
        }
    }

    public void BaseSkill(AnimationEvent myEvent)
    {
        int index = myEvent.intParameter - 1;
        _ps = _skillEffect[index].GetComponent<ParticleSystem>();
        StopCoroutine(SkillOff(_skillEffect[index], index));
        _skillEffect[index].SetActive(true);
        _ps.Play();

        skillInfoData = _player.Data.SkillData.GetSkillInfo(myEvent.intParameter);

        _player.Stats.ChangeManaAction(-skillInfoData.ManaCost);
        _player.PlayerSkills.coolDowns[index] = _player.Data.SkillData.GetSkillInfo(myEvent.intParameter).CoolDown;
        StartCoroutine(SkillOff(_skillEffect[index], index));
    }

    IEnumerator SkillOff(GameObject obj, int index)
    {
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        float duration = _player.Data.SkillData.GetSkillInfo(index + 1).Duration;
        if (duration != 0)
        {
            float addAttack = _player.Stats.attack * 0.5f;
            _player.Stats.attack += addAttack;
            yield return new WaitForSeconds(duration);
            _player.Stats.attack -= addAttack;
        }
        else
        {
            yield return new WaitForSeconds(ps.main.duration);
        }
        ps.Stop();
        obj.SetActive(false);   
    }

    public void SkillAttack(AnimationEvent myEvent)
    {
        SkillInfoData skillInfoData = _player.Data.SkillData.GetSkillInfo(myEvent.intParameter);
        float range = skillInfoData.SkillRange;
        var forward = _player.transform.forward;
        forward.Normalize();
        Vector3 attackPos = _player.transform.position + new Vector3(0, 1.1f, 0) + forward * range;
        Collider[] colliders = Physics.OverlapSphere(attackPos, range);
        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.GetComponent<IDamagable>() != null && collider.gameObject != _playerObj)
                {
                    collider.GetComponent<IDamagable>().TakePhysicalDamage(skillInfoData.Damage * (int)_player.Stats.attack);
                }
            }
        }
    }
}
