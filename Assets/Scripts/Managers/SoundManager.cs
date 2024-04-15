using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    private SoundObject soundObject;

    public void PlayHitSound(int index)
    {
    }

    public void PlayAttackSound(int index)
    {
        AudioClip clip = Resources.Load("Sound/TestSound") as AudioClip;
        PlaySound(clip);
    }

    private void PlaySound(AudioClip clip)
    {
        soundObject = PoolManager.Instance.SpawnFromPool("SoundObject").GetComponent<SoundObject>();
        soundObject.PlaySound(clip);
    }
}
