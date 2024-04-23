using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDieEvent : MonoBehaviour
{
    [SerializeField] private GameObject _portal;
    private CharacterStats _characterStats;

    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
        _characterStats.OnDie += () => _portal.SetActive(true);
    }
}
