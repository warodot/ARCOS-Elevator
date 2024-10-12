using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BulletWhizzle : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources = new();
    [SerializeField] List<AudioClip> audioClips = new();


    public void FireSFX()
    {
        audioSources[Random.Range(0, 2)].PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
    }
}
