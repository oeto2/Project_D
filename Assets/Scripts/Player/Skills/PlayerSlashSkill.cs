using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerSlashSkill : MonoBehaviour
{
    private ParticleSystem _ps;
    private List<GameObject> _enemies = new List<GameObject>();
    private List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();
    private List<ParticleSystem.Particle> _inside = new List<ParticleSystem.Particle>();
    private GameObject _player;
    private SkillInfoData _skillInfoData;
    private PlayerSkills _playerSkills;

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        _player = GameManager.Instance.playerObject;
        _playerSkills = _player.GetComponentInChildren<PlayerSkills>();
        _skillInfoData = _player.GetComponent<Player>().Data.SkillData.GetSkillInfo(1);
    }

    private void OnEnable()
    {
        _enemies.Clear();
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    if (other?.GetComponent<IDamagable>() != null)
    //    {
    //        if (!_playerSkills._enemies.Contains(other))
    //        {
    //            other.GetComponent<IDamagable>().TakePhysicalDamage(_skillInfoData.Damage);
    //            _playerSkills._enemies.Add(other);
    //        }
    //    }
    //}

    //private void OnParticleCollision(GameObject other)
    //{
    //    _ps.GetCollisionEvents(other, _collisionEvents);
    //    Debug.Log(_collisionEvents.Count);
    //    int i = 0;
    //    while (i < _collisionEvents.Count)
    //    {
    //        var obj = _collisionEvents[i].colliderComponent.gameObject;
    //        if (obj?.GetComponent<IDamagable>() != null)
    //        {
    //            if (!_enemies.Contains(obj))
    //            {
    //                obj.GetComponent<IDamagable>().TakePhysicalDamage(_skillInfoData.Damage);
    //                _enemies.Add(obj);
    //            }
    //        }
    //        i++;
    //    }
    //}

    //private void OnParticleTrigger()
    //{
    //    Debug.Log("파티클 트리거");
    //    _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, _inside);
    //
    //    foreach (var e in _inside)
    //    {
    //        Debug.Log(e);
    //    }
    //}
}
