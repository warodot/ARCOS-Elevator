using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tellory.UI.RingMenu;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DH_UIManager : MonoBehaviour
{
    [Header("Interaction Related")]
    public CanvasGroup m_centerPoint;
    public TextMeshProUGUI m_textMessege;
    CanvasGroup m_textCanvasGroup;
    public float m_duration;

    [Space]
    [Header("Tutorial")]
    public CanvasGroup m_iconKeyToClose;
    public float m_durationTutorial = 1;
    CanvasGroup m_textStopCavnas;
    public TextMeshProUGUI m_textStopInteract;

    [Space]
    [Header("Mouse Appareance")]
    public Sprite m_default;
    public Sprite m_take;

    Coroutine m_detectCor;
    Coroutine m_tutorialCor;
    void Awake() 
    {
        m_textCanvasGroup = m_textMessege.GetComponent<CanvasGroup>();
        m_textStopCavnas = m_textStopInteract.GetComponent<CanvasGroup>();
    } 

    void Update()
    {
        DetectState();
        if (DH_Interact.m_isDetectingObject) m_textMessege.text = DH_Interact.m_message;
    }

    void DetectState()
    {
        if (DH_GameManager.State == GameStates.Mirilla)
        {
            m_textStopInteract.text = "Para dejar de mirar";
            if (Input.GetKeyDown(KeyCode.Q)) DH_GameManager.State = GameStates.Gameplay;
        }
    }

    void OnEnable() 
    {
        DH_GameManager.StateAction += CanvasState;
        DH_Interact.IsDetecting += DetectInteractable;
    } 

    void OnDisable() 
    {
        DH_GameManager.StateAction -= CanvasState;
        DH_Interact.IsDetecting -= DetectInteractable;
    }

    void DetectInteractable(bool detecting)
    {
        if (m_detectCor != null)  StopCoroutine(m_detectCor);

        if (detecting) m_detectCor = StartCoroutine(DetectingBehavior(true));
        else m_detectCor = StartCoroutine(DetectingBehavior(false));
    }

    void CanvasState(GameStates state)
    {
        if (m_tutorialCor != null) StopCoroutine(m_tutorialCor);

        if (state == GameStates.Mirilla) m_tutorialCor = StartCoroutine(ExitInteracting(1));
        else if (state == GameStates.Gameplay) m_tutorialCor = StartCoroutine(ExitInteracting(0));
    }

    IEnumerator ExitInteracting(float target)
    {
        float initial = m_iconKeyToClose.alpha;
        
        for (float i = 0; i < m_durationTutorial; i+= Time.deltaTime)
        {
            float t = i / m_durationTutorial;
            m_iconKeyToClose.alpha = Mathf.Lerp(initial, target, t);
            m_textStopCavnas.alpha = Mathf.Lerp(initial, target, t);
            yield return null;
        }

        m_iconKeyToClose.alpha = target;
        m_textStopCavnas.alpha = target;
    }

    IEnumerator DetectingBehavior(bool isDetecting)
    {
        float initial = m_centerPoint.alpha;
        float target = isDetecting ? 1 : 0;
        
        for (float i = 0; i < m_duration; i += Time.deltaTime)
        {
            float t = i / m_duration;
            m_centerPoint.alpha = Mathf.Lerp(initial, target, t);
            m_textCanvasGroup.alpha = Mathf.Lerp(initial, target, t);
            yield return null;
        }

        m_centerPoint.alpha = target;
        m_textCanvasGroup.alpha = target;
    }
}
