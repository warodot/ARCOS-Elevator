using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FootStepsRelated
{
    public List<AudioClip> woodGround;
    public List<AudioClip> metalGround;
    public List<AudioClip> other;
}

public enum TypeOfGround
{
    wood,
    metal,
    other
}

public class DH_CharacterSoundsEmiter : MonoBehaviour
{
    [Header("Sources")]
    public DH_AudioManager m_audioManager;
    public AudioSource m_footStepsSource;

    [Space]
    [Header("Footsteps")]
    public FootStepsRelated m_footSteps;
    public float m_speed;
    
    [Space]
    [Header("Interact")]
    public AudioClip m_interact;

    float constantValue = 0;
    float currentStepSpeed = 0;
    TypeOfGround m_gorund;
    public void Footsteps(CharacterState m_charState)
    {
        List<AudioClip> m_clips = m_gorund switch
        {
            TypeOfGround.wood => m_footSteps.woodGround,
            TypeOfGround.metal => m_footSteps.metalGround,
            TypeOfGround.other => m_footSteps.other,
            _ => default
        };

        switch(m_charState)
        {
            case CharacterState.Walking: currentStepSpeed = m_speed; break;
            case CharacterState.Running: currentStepSpeed = m_speed / 2; break;
            case CharacterState.Crouching: currentStepSpeed = m_speed * 2; break;
            case CharacterState.Idle: currentStepSpeed = 0; break;
        }

        if (m_charState != CharacterState.Idle)
        {
            constantValue += Time.deltaTime;
            if (constantValue > currentStepSpeed)
            {
                constantValue = 0;
                m_footStepsSource.PlayOneShot(m_clips[0]);
                m_audioManager.VariablePitch("FootSteps", 0.8f, 1.5f);
            }
        }
    }
}
