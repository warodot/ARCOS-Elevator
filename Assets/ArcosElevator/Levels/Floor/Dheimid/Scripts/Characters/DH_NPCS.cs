using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DH_Attributes;
using UnityEngine;

public class DH_NPCS : MonoBehaviour
{
    Animator anim;
    public Transform initial;
    public Transform target;
    public float m_maxDistance = 2.5f;

    void Start() => anim = GetComponent<Animator>();

    float weight;
    void OnAnimatorIK(int layerIndex)
    {
        float distance = Vector3.Distance(initial.position, target.position);
        weight = distance < m_maxDistance ? weight + Time.deltaTime * 2 : weight - Time.deltaTime * 2;

        weight = Mathf.Clamp01(weight);
        anim.SetLookAtWeight(weight);
        anim.SetLookAtPosition(target.position);
    }

    [Button]
    public void PosBackTarget(string nameOfAnimation)
    {
        if (gameObject.activeSelf == false) gameObject.SetActive(true);
        transform.position = target.transform.parent.position + -target.transform.parent.forward * 1.5f;
        transform.rotation = target.transform.parent.rotation;
        anim.CrossFade(nameOfAnimation, 0.2f);
    }
}
