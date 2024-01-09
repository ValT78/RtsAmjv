using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip fireClip;

    public enum SoundType
    {
        fire
    }

    private void Start()
    {
    }

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.fire:
                audioSource.PlayOneShot(fireClip);
                break;
        }
    }
}
