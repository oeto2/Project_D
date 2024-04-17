using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class HitDamageText : MonoBehaviour
{
    private TMP_Text _hitDamageTmp;

    private void Awake()
    {
        _hitDamageTmp = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartTextAniamtion());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private IEnumerator StartTextAniamtion()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.005f);
        float upPos = 0;

        while(upPos < 0.1)
        {
            upPos += 0.001f;
            transform.position += Vector3.up * upPos;
            yield return waitForSeconds;
        }

        gameObject.SetActive(false);
    }

    public void SetHitDamageText(int damage_)
    {
        _hitDamageTmp.text = damage_.ToString();
    }
}
 