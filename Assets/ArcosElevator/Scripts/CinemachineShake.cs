using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance{get; private set;}
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;
    private float _startingFrequency;
    
    void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    /// <summary>
    /// Shakes the camera using the Noise property of Cinemachine Virtual Camera
    /// </summary>
    /// <param name="intensity"> Amplitude of the noise parameter, how much camera shake should there be </param>
    /// <param name="frequency"> Frequency of the noise parameter, how fast should camera shake be </param>
    /// <param name="time"> How much time passes before returning to 0 </param>
    public void ShakeCamera(float intensity, float frequency, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        _startingIntensity = intensity;
        _startingFrequency = frequency;
        _shakeTimerTotal = time;
        _shakeTimer = time;
    }

    void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            //Time over
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = Mathf.Lerp(_startingFrequency, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
        }
    }
}
