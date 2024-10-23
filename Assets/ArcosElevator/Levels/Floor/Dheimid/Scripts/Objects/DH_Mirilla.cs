using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DH_Mirilla : MonoBehaviour, DH_IinteractableObject
{
    public GameObject m_camera;
    public bool activeMirilla;

    public UnityEvent m_actionToDisableMirilla;

    void OnEnable() => DH_GameManager.StateAction += State;

    void OnDisable() => DH_GameManager.StateAction -= State;

    public void Interact()
    {
        if (activeMirilla) 
        {
            if (m_camera) m_camera.SetActive(true);
            DH_GameManager.State = GameStates.Mirilla;
        }
        else
        {
            m_actionToDisableMirilla?.Invoke();
        }
    }

    void State(GameStates state)
    {
        if (state != GameStates.Mirilla) if (m_camera) m_camera.SetActive(false);
    }

    public void MirillaBehavior(bool active)
    {
        activeMirilla = active;
    }
}
