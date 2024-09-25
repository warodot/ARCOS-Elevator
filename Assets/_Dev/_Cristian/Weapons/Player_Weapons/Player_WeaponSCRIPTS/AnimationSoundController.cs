using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips = new();
    [SerializeField] AudioSource source;
    private int _currentSFX;


    void PlaySFX()
    {
        source.Stop();
        source.clip = audioClips[_currentSFX];
        source.Play();
        _currentSFX++;
    }

    void ResetSFX()
    {
        _currentSFX = 0;
    }
}

