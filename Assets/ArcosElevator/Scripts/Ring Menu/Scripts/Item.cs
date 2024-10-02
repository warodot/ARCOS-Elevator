using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ARCOS Elevator/Item")]
public class Item : ScriptableObject
{
    // Variables
    [SerializeField] private string m_name;
    [SerializeField] private Sprite m_icon;
    [SerializeField] private AudioClip m_pickupSound;
    [SerializeField] private GameObject m_prefab;


    // Properties
    public string Name => m_name;
    public Sprite Icon => m_icon;
    public GameObject Prefab => m_prefab;
    public AudioClip PickupSound => m_pickupSound;

    // Constructor
    public Item(string name, Sprite icon, AudioClip pickupSound, GameObject prefab)
    {
        m_name = name;
        m_icon = icon;
        m_prefab = prefab;
        m_pickupSound = pickupSound;
    }

}
