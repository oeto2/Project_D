using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    //몇 초뒤에 이펙트를 끌것인지
    [SerializeField] private float falseTime = 3f;

    void OnEnable()
    {
        StartCoroutine(EffectActiveFalse());
    }

    private IEnumerator EffectActiveFalse()
    {
        yield return new WaitForSeconds(falseTime);
        gameObject.SetActive(false);
    }
}
