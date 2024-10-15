using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puerto : MonoBehaviour
{
    [SerializeField] Transform boxPosition;
    [SerializeField] Caja caja;

    public UnityEvent openDoor;
    public UnityEvent closeDoor;

    private void Update()
    {
        if(caja != null && caja.IsGet == true)
        {
            caja = null;
            closeDoor?.Invoke();
        }
    }

    public void Activation()
    {
        //colocar la caja en el puerto
        if(Baston.instance.obj == null) return;

        GameObject obj = Baston.instance.obj;
        Baston.instance.LeaveBox();
        caja = obj.GetComponent<Caja>();

        obj.transform.position = boxPosition.position;
        obj.transform.rotation = boxPosition.rotation;
        //abrir la puerta

        openDoor?.Invoke();
    }
}
