using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tellory.UI.RingMenu;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum DH_StateUI
{
    AddedToInventory,
    InSuitcase,
    none,
}

public class DH_UIManager : MonoBehaviour
{
    public static Action<DH_StateUI, string> ActionState;

    [Header("Interaction Related")]
    public CanvasGroup m_centerPoint;
    public TextMeshProUGUI m_textMessege;
    CanvasGroup m_textCanvasGroup;
    public float m_duration;

    [Space]
    [Header("Tutorial")]
    public CanvasGroup m_canvasGroupTutorial;
    public TextMeshProUGUI m_textTutorial;

    [Space]
    [Header("Added To Inventory")]
    public Transform m_parent;
    public GameObject m_prefab;
    public CanvasGroup m_canvasAddedToInventory;

    [Space]
    [Header("Mouse Appareance")]
    public Sprite m_default;
    public Sprite m_take;

    Coroutine m_detectCor;
    Coroutine m_tutorialCor;
    void Awake() 
    {
        m_textCanvasGroup = m_textMessege.GetComponent<CanvasGroup>();
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
            m_textTutorial.text = "Para dejar de mirar";
            if (Input.GetKeyDown(KeyCode.Q)) DH_GameManager.State = GameStates.Gameplay;
        }
    }

    void OnEnable() 
    {
        DH_GameManager.StateAction += CanvasState;
        DH_Interact.IsDetecting += DetectInteractable;
        ActionState += StateUI;
    } 

    void OnDisable() 
    {
        DH_GameManager.StateAction -= CanvasState;
        DH_Interact.IsDetecting -= DetectInteractable;
        ActionState -= StateUI;
    }

    void StateUI(DH_StateUI state, string text)
    {
        if (state == DH_StateUI.InSuitcase)
        {
            m_textTutorial.text = text; 
            StartCoroutine(ShowCanvas(m_canvasGroupTutorial, null));
        }
        else if (state == DH_StateUI.AddedToInventory)
        {
            GameObject prefab = Instantiate(m_prefab, m_parent);
            prefab.GetComponent<TextMeshProUGUI>().text = text;
            StartCoroutine(ShowCanvas(m_canvasAddedToInventory, prefab));
        }
    }

    void DetectInteractable(bool detecting)
    {
        if (m_detectCor != null)  StopCoroutine(m_detectCor);

        if (detecting) m_detectCor = StartCoroutine(DetectingBehavior(true));
        else m_detectCor = StartCoroutine(DetectingBehavior(false));
    }

    void CanvasState(GameStates state)
    {
       // if (m_tutorialCor != null) StopCoroutine(m_tutorialCor);

        // if (state == GameStates.Mirilla) m_tutorialCor = StartCoroutine(ExitInteracting(1));
        // else if (state == GameStates.Gameplay) m_tutorialCor = StartCoroutine(ExitInteracting(0));
    }

    // IEnumerator ExitInteracting(float target)
    // {
    //     float initial = m_canvasGroupTutorial.alpha;
        
    //     for (float i = 0; i < m_durationTutorial; i+= Time.deltaTime)
    //     {
    //         float t = i / m_durationTutorial;
    //         m_canvasGroupTutorial.alpha = Mathf.Lerp(initial, target, t);
    //         yield return null;
    //     }

    //     m_canvasGroupTutorial.alpha = target;
    // }

    IEnumerator ShowCanvas(CanvasGroup canvasG, GameObject toDestroy)
    {
        for (float i = 0; i < 1; i+= Time.deltaTime)
        {
            float t = i / 1;
            canvasG.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        canvasG.alpha = 1;

        yield return new WaitForSeconds(4);

        for (float i = 0; i < 1; i+= Time.deltaTime)
        {
            float t = i / 1;
            canvasG.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        canvasG.alpha = 0;
        if (toDestroy != null) Destroy(toDestroy);
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
