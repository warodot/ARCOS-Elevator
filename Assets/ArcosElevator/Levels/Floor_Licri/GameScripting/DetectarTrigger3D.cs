using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Evento
{
    public string tag;
    public UnityEvent eventos;

    public void ChequearEvento(string _tag)
    {
        if (_tag == tag)
            eventos.Invoke();
    }

}

public class DetectarTrigger3D : MonoBehaviour
{

    public List<Evento> onEnter = new List<Evento>();
    public List<Evento> onStay = new List<Evento>();
    public List<Evento> onExit = new List<Evento>();

    private void OnTriggerEnter(Collider collision)
    {
        RecorrerLista(onEnter, collision.tag);
    }

    private void OnTriggerStay(Collider collision)
    {
        RecorrerLista(onStay, collision.tag);
    }

    private void OnTriggerExit(Collider collision)
    {
        RecorrerLista(onExit, collision.tag);
    }

    void RecorrerLista(List<Evento> _evento, string _tag)
    {
        for (int i = 0; i < _evento.Count; i++)
        {
            _evento[i].ChequearEvento(_tag);
        }
    }
}
