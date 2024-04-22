using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitUI : MonoBehaviour
{
    private Image _bloodScreen;
    private Color color;

    void Start()
    {
        _bloodScreen = GetComponent<Image>();
        GameManager.Instance.player.Stats.OnDamage += OnHitBloodOverlay;
        color = _bloodScreen.color;
    }

    private void OnHitBloodOverlay(int amount)
    {
        StopCoroutine(OffBloodScreen());
        color.a = 1f;
        _bloodScreen.color = color;
        StartCoroutine(OffBloodScreen());
    }

    IEnumerator OffBloodScreen()
    {
        float curTime = 0f;
        float maxTime = 1f;

        while (curTime < maxTime)
        {
            color.a = 1f - (curTime / maxTime);
            _bloodScreen.color = color;
            curTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        _bloodScreen.color = color;
    }
}
