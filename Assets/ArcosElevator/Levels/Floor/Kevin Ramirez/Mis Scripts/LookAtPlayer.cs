using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform mirarObjetivo;
    public Transform cabezaDelAgente;
    public float smoothRotationOnEnter;

    void Update()
    {
        Quaternion rotacionCabezaObjetivo = Quaternion.LookRotation(mirarObjetivo.position - cabezaDelAgente.position);
        cabezaDelAgente.rotation = Quaternion.Lerp(cabezaDelAgente.rotation, rotacionCabezaObjetivo, smoothRotationOnEnter);

        Vector3 limitRotationHead = cabezaDelAgente.localEulerAngles;

        if (limitRotationHead.x > 90f && limitRotationHead.x < 180f)
        {
            limitRotationHead.x = 90f;
        }
        if (limitRotationHead.x < 270f && limitRotationHead.x > 180f)
        {
            limitRotationHead.x = 270f;
        }

        if (limitRotationHead.y > 90f && limitRotationHead.y < 180f)
        {
            limitRotationHead.y = 90f;
        }
        if (limitRotationHead.y < 270f && limitRotationHead.y > 180f)
        {
            limitRotationHead.y = 270f;
        }

        if (limitRotationHead.z > 90f && limitRotationHead.z < 180f)
        {
            limitRotationHead.z = 90f;
        }
        if (limitRotationHead.z < 270f && limitRotationHead.z > 180f)
        {
            limitRotationHead.z = 270f;
        }

        cabezaDelAgente.localEulerAngles = limitRotationHead;
    }
}
