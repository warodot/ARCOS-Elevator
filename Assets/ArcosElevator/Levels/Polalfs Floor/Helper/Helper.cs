using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Helper : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private NavMeshAgent m_agent;

    [Header("Interact")]
    [SerializeField] private float m_interactRange;
    [SerializeField] private LayerMask m_interactMask;
    private InteractableObject _interactable;
    private bool _canInteract;

    [Header("Grab")]
    [SerializeField] private GameObject m_box;
    [SerializeField] private Transform m_dropSpawn;
    private Vector3 _dropPoint;
    private bool _drop;

    [Header("Visuals")]
    [SerializeField] private Material m_light;
    [SerializeField] private Color m_lightColorBase;
    [SerializeField] private Color m_lightColorRed;
    [SerializeField] private Color m_lightColorGreen;

    //Properties
    public GameObject Box => m_box;

    private void Start()
    {
        m_agent.stoppingDistance = m_interactRange - 0.5f;
    }
    private void Update()
    {
        if (_interactable == null) _canInteract = false;
        if(_interactable != null)
        {
            float dist = Vector3.Distance(transform.position, _interactable.transform.position);
            if(dist < m_interactRange)
            {
                transform.LookAt(new Vector3(_interactable.transform.position.x,transform.position.y, _interactable.transform.position.z));
                if(_canInteract) StartCoroutine(Interactuar());


            }
        }
        if(_drop)
        {
            float distDrop = Vector3.Distance(transform.position, _dropPoint);
            if (distDrop < m_interactRange)
            {
                Drop();
            }
        }
    }


    public void SetMove(Vector3 target)
    {
        StartCoroutine(LightAnim(m_lightColorGreen));
        target = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(target);
        m_agent.SetDestination(target);
    }


    public void SetInteractable(InteractableObject _interactable)
    {
        this._interactable = _interactable;
        _canInteract = true;
    }

    public void SetDrop(Vector3 dropTarget)
    {
        if (m_box == null)
        {
            StartCoroutine(LightAnim(m_lightColorRed));
            return;
        }
        _drop = true;
        _dropPoint = dropTarget;
    }

    private void Drop()
    {
        if(m_box == null) return;
        m_agent.SetDestination(transform.position);
        m_box.SetActive(true);
        m_box.transform.position = m_dropSpawn.position;
        _drop = false;
        m_box = null;
      
        return;
    }

    private void ChangeLightColor(Color color)
    {
        m_light.SetColor("_EmissionColor", color * 5);
    }
    private IEnumerator Interactuar()
    {
        _canInteract = false;    
        m_agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        if(_interactable.ShowType() == TypeOfInteract.Grab)
        {
            m_box = _interactable.gameObject;
            m_box.gameObject.SetActive(false);
            _interactable = null;
        }
        else if(_interactable.ShowType() == TypeOfInteract.Input)
        {
            _interactable.GetComponent<Interactable>().Interact();
            _interactable = null;
            Debug.Log("Lo presiono");
        }
        _canInteract = true;
        m_agent.isStopped = false;
    }

    private IEnumerator LightAnim(Color color)
    {
        for (int i = 0; i < 5; i++)
        {
            ChangeLightColor(color);
            yield return new WaitForSeconds(0.2f);
            ChangeLightColor(m_lightColorBase);
        }
        ChangeLightColor(m_lightColorBase);
    }


}
