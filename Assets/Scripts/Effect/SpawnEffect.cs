using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    //�� �ʵڿ� ����Ʈ�� ��������
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
