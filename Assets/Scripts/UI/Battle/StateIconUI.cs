using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIconUI : MonoBehaviour
{
    [SerializeField] private GameObject _bleedIcon;
    private CharacterStats _playerStats;

    private void Start()
    {
        _playerStats = GameManager.Instance.player.Stats;
        _playerStats.OnBleed += bleedIcon;
    }

    private void bleedIcon(bool icon)
    {
        if (icon)
            _bleedIcon.SetActive(true);
        else
            _bleedIcon.SetActive(false);
    }
}
