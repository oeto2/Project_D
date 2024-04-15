using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();

        Invoke("Disable", clip.length);
    }

    private void Disable()
    {
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}
