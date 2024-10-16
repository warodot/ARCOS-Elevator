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
    public LayerMask m_layerDors;
    
    [Space]
    [Header("movement")]
    public float speed;

    void Start() => anim = GetComponent<Animator>();

    void Update()
    {
        if (isMoving && !isOpen) DetectDoors();
    }

    bool isOpen;
    DH_Door m_door;
    void DetectDoors()
    {
        Vector3 yPos = transform.position + new Vector3(0, 0.5f, 0);

        if (Physics.Raycast(yPos, transform.forward, out RaycastHit hit, 1.2f, m_layerDors))
        {
            if (hit.collider.CompareTag("DH_Door"))
            {
                hit.collider.TryGetComponent(out DH_Door door);
                if (!door.isOpen && !door.isLocked) door.Interact();
                Invoke(nameof(CloseDoor), 2f);
                m_door = door;
                isOpen = true;
            }

            Debug.DrawRay(yPos, transform.forward * 1.2f, Color.red);
        }
    }

    void CloseDoor()
    {
        m_door.Interact();
        isOpen = false;
    }

    float weight;
    void OnAnimatorIK(int layerIndex)
    {
        float distance = Vector3.Distance(initial.position, target.position);
        weight = distance < m_maxDistance ? weight + Time.deltaTime * 2 : weight - Time.deltaTime * 2;

        weight = Mathf.Clamp01(weight);
        anim.SetLookAtWeight(weight);
        anim.SetLookAtPosition(target.position);
    }

    public void PosBackTarget(string nameOfAnimation)
    {
        if (gameObject.activeSelf == false) gameObject.SetActive(true);
        transform.position = target.transform.parent.position + -target.transform.parent.forward * 1.1f;
        transform.rotation = target.transform.parent.rotation;
        anim.CrossFade(nameOfAnimation, 0.2f);
    }

    public void StartMovement(DH_WalkableZones zones) => StartCoroutine(GoToPosition(zones, 0));

    bool isMoving;
    IEnumerator GoToPosition(DH_WalkableZones zones, int value)
    {
        if (value > zones.finalPostions.Count - 1) 
        {
            isMoving = false;
            anim.CrossFade("Look At", 0.2f);
            yield break;
        }

        isMoving = true;
        // Mirar a la zona deseada -------------

            anim.CrossFade("Walk", 0.2f); //Animación

            Quaternion qInitial = transform.rotation;
            Vector3 lookAt = (zones.finalPostions[value] - transform.position).normalized;
            Quaternion qLookAt = Quaternion.LookRotation(lookAt);
            Vector3 lookAtEulerAngles = qLookAt.eulerAngles;
            Quaternion desireRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, lookAtEulerAngles.y, transform.eulerAngles.z));

        //--------------------------

        Vector3 initial = transform.position;
        Vector3 target = zones.finalPostions[value];

        for (float i = 0; i < 2; i+=Time.deltaTime)
        {
            float t = i/ 2;
            transform.SetPositionAndRotation(Vector3.Lerp(initial, target, t), Quaternion.Lerp(qInitial, desireRotation, t * 4));
            yield return null;
        }

        transform.SetPositionAndRotation(target, desireRotation);
        StartCoroutine(GoToPosition(zones, value+1));
    }

    public void ChangeAnimation(string animation) => anim.CrossFade(animation, 0.2f);
    
}
