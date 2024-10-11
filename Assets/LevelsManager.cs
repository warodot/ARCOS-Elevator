using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance;
    
    public CanvasGroup m_canvas;

    //Se parte por defecto en alpha 1.
    void Awake()
    {
        m_canvas.alpha = 1;
        Instance = this;
    }

    //Se inicia el fade.
    void Start() => StartCoroutine(BackgroundBehavior(SceneManager.GetActiveScene().name, true));

    //Funcion publica para cambiar el nivel en base a un nombre.
    public void ChangeLevel(string name) => StartCoroutine(BackgroundBehavior(name, false));

    //Comportamiento de transición
    IEnumerator BackgroundBehavior(string name, bool active)
    {
        float inital = m_canvas.alpha;
        float target = active ? 0 : 1;

        for (float i = 0; i < 2; i+=Time.deltaTime)
        {
            float t = i / 2;
            m_canvas.alpha = Mathf.Lerp(inital, target, t);
            yield return null;
        }

        m_canvas.alpha = target;

        if (target == 1) SceneManager.LoadScene(name);
    }
}
