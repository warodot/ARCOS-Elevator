using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        //cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Método para activar la sacudida de cámara
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachinePerlin = 
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachinePerlin.m_AmplitudeGain = intensity; // Ajustar la intensidad de la sacudida
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            // Lerp para reducir la intensidad de la sacudida con el tiempo
            CinemachineBasicMultiChannelPerlin cinemachinePerlin = 
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachinePerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));

            if (shakeTimer <= 0f)
            {
                // Cuando termina el tiempo de sacudida, apagar el efecto
                cinemachinePerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
