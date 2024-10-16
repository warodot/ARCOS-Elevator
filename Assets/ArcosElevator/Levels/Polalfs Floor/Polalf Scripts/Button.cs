using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Door m_door;
    [SerializeField] private bool m_canOpen;

    [Header("Visuals")]
    [SerializeField] private Material m_lightA;
    [SerializeField] private Material m_lighBt;
    [SerializeField] private MeshRenderer m_renderer;

    public void ToggleOpen(bool open)
    {
        m_canOpen = open;
    }

    public void ChangeColor(Material material)
    {
        if(!m_canOpen) return;
        m_door.Open();
        m_renderer.material = material;
    }
}
