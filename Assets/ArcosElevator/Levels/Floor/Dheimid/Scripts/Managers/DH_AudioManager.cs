using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DH_AudioManager : MonoBehaviour
{
    public AudioMixer m_mixer;
    public AudioMixerGroup m_defaultGroup;
    public float timeSnpshotTransition = 0.5f;

    [Serializable]
    public class VolumesMixer
    {
        [Range(0, 1)] public float m_generalVolume = 1; 
        [Range(0, 1)] public float m_MusicVolume = 1; 
        [Range(0, 1)] public float m_SoundEffectsVolume = 1; 
        [Range(0, 1)] public float m_EnvironmentVolume = 1; 
    }
    
    [Space]
    public VolumesMixer m_mixerGeneralVolumes;

    void Update()
    {
        //Setear volumenes basado en los parametros expuestos en el Audio Mixer
        SetVolumesParameter();
    }

    void SetVolumesParameter()
    {
        SetVolume(m_mixerGeneralVolumes.m_generalVolume, "General Volume");
        SetVolume(m_mixerGeneralVolumes.m_MusicVolume, "General Music Volume");
        SetVolume(m_mixerGeneralVolumes.m_SoundEffectsVolume, "General Sound Effects Volume");
        SetVolume(m_mixerGeneralVolumes.m_EnvironmentVolume, "General Environment Volume");
    }

    public void TransitionToSnapshot(AudioMixerSnapshot m_snapShot)
    {
        m_snapShot.TransitionTo(timeSnpshotTransition);
    }

    public void DramaticStop(AudioSource m_source)
    {
        StartCoroutine(StopDramaticGroup(m_source));
    }

    IEnumerator StopDramaticGroup(AudioSource m_soruce)
    {
        float initial = m_soruce.pitch;
        float target = 0;
        
        for (float i = 0; i < 5; i+=Time.deltaTime)
        {
            float t = i / 5f;
            m_soruce.pitch = Mathf.Lerp(initial, target, t);
            yield return null;
        }

        m_soruce.pitch = target;
        m_soruce.Stop();
    }

    // Método para ajustar el volumen
    public void SetVolume(float normalizedVolume, string parameterName)
    {
        // Transforma el valor normalizado al rango de -80 a 0 dB (Audio mixer manera volumenes de -80db a 0db)
        // Sin embargo, el oido humano deja de escucharlo en -45, asi que en ese valor se pondrá por default.
        float volume = Mathf.Lerp(-45f, 0f, normalizedVolume);
        
        // Ajusta el volumen en el AudioMixer
        m_mixer.SetFloat(parameterName, volume);
    }

    public void VariablePitch(string exposedNameAudioMixer, float min, float max)
    {
        float value = UnityEngine.Random.Range(min, max);
        m_mixer.SetFloat(exposedNameAudioMixer, value);
    }
}
