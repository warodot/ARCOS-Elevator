using System.Collections;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    // Lista de clips de audio
    public AudioClip[] audioClips;

    // Fuente de audio
    [SerializeField]private AudioSource audioSource;

    // Rango de tiempo mínimo y máximo aleatorio
    public float minMinTime = 1f; // Valor mínimo para el tiempo mínimo entre reproducciones
    public float maxMinTime = 5f; // Valor máximo para el tiempo mínimo entre reproducciones

    public float minMaxTime = 6f; // Valor mínimo para el tiempo máximo entre reproducciones
    public float maxMaxTime = 15f; // Valor máximo para el tiempo máximo entre reproducciones

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Iniciar la reproducción aleatoria
        StartCoroutine(PlayRandomAudio());
    }

    IEnumerator PlayRandomAudio()
    {
        while (true)
        {
            // Seleccionar un clip aleatorio
            AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];

            // Reproducir el clip aleatorio
            audioSource.clip = randomClip;
            audioSource.Play();

            // Esperar hasta que el clip termine
            yield return new WaitForSeconds(randomClip.length);

            // Generar tiempos mínimos y máximos aleatorios
            float currentMinTime = Random.Range(minMinTime, maxMinTime);
            float currentMaxTime = Random.Range(minMaxTime, maxMaxTime);

            // Calcular el próximo tiempo de espera como un valor aleatorio entre esos tiempos
            float nextPlayTime = Random.Range(currentMinTime, currentMaxTime);

            // Esperar antes de reproducir el siguiente audio
            yield return new WaitForSeconds(nextPlayTime);
        }
    }
}