using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DH_AudioManager : MonoBehaviour
{
    public AudioMixer m_mixer;
    public AudioMixerGroup m_defaultGroup;
    public float defaultTimeTransition = 0.5f;

    [Range(0, 1)] public float m_generalVolume; 
    [Range(0, 1)] public float m_generalMusicVolume; 
    [Range(0, 1)] public float m_generalSoundEffectsVolume; 
    [Range(0, 1)] public float m_generalEnvironmentVolume; 

    void Update()
    {
        //Setear volumenes basado en los parametros expuestos en el Audio Mixer
        SetVolumesParameter();
    }

    void SetVolumesParameter()
    {
        SetVolume(m_generalVolume, "General Volume");
        SetVolume(m_generalMusicVolume, "General Music Volume");
        SetVolume(m_generalSoundEffectsVolume, "General Sound Effects Volume");
        SetVolume(m_generalEnvironmentVolume, "General Environment Volume");
    }

    public void TransitionToSnapshot(AudioMixerSnapshot m_snapShot)
    {
        m_snapShot.TransitionTo(defaultTimeTransition);
    }

    // Método para ajustar el volumen
    public void SetVolume(float normalizedVolume, string parameterName)
    {
        // Transforma el valor normalizado al rango de -80 a 0 dB (Audio mixer manera volumenes de -80db a 0db)
        float volume = Mathf.Lerp(-45f, 0f, normalizedVolume);
        
        // Ajusta el volumen en el AudioMixer
        m_mixer.SetFloat(parameterName, volume);
    }
}
