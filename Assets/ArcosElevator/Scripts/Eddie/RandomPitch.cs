using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    // Variables to control pitch range
    public AudioSource audioSource;  // The AudioSource component
    public float minPitch = 0.8f;    // Minimum pitch value
    public float maxPitch = 1.2f;    // Maximum pitch value

    void Start()
    {
        // Check if the AudioSource is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        // Set a random pitch within the specified range
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        // Play the audio clip
        audioSource.Play();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
         float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;
    }
}

