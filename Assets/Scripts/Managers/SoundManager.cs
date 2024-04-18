using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    private SoundObject soundObject;
    System.Random random = new System.Random();

    public void PlayHitSound(int index)
    {
        int idx = index;
        if (idx == 0)
            idx = random.Next(1, 4);

        AudioClip clip = Resources.Load($"Sound/HitAttack{idx}") as AudioClip;
        PlaySound(clip);
    }

    public void PlayAttackSound(int index)
    {
        AudioClip clip = Resources.Load($"Sound/BaseAttack{index}") as AudioClip;
        PlaySound(clip);
    }

    public void PlayGuardSound()
    {
        AudioClip clip = Resources.Load("Sound/Guard") as AudioClip;
        PlaySound(clip);
    }

    private void PlaySound(AudioClip clip)
    {
        soundObject = PoolManager.Instance.SpawnFromPool("SoundObject").GetComponent<SoundObject>();
        soundObject.PlaySound(clip);
    }
}
