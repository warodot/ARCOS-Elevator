using System.Collections;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System;

public class DH_SuitcaseInventory : MonoBehaviour
{
    Animator m_anim;
    CinemachineVirtualCamera m_virtual;
    Vector3 initialPosition;
    Quaternion initialRotation;
    Vector3 cameraInitialPos;

    [Header("Name animations")]
    public string m_openAnimationName;
    public string m_closeAnimationName;

    [Space]

    [Header("Times and offset")]
    public float m_timeToPutInGround;
    public float forwardOffset;

    [Space]

    [Header("Camera related")]
    public List<GameObject> m_cameras;
    public GameObject m_currentCamera;
    public AnimationCurve m_curve;

    [Space]

    [Header("posiciones")]
    public GameObject m_player;
    
    [Space]

    [Header("Spaces of inventory")]
    public DH_Inventory m_inventory;
    public List<GameObject> m_spaces;
    public float m_maxDistance = 2f;
    public LayerMask m_layer;
    public Texture2D m_default;
    public Texture2D m_detecting;

    public Action<GameObject> Tool;
    public bool InSuitcase;
    public bool Opening;
    public bool canOperate;
    
    void Start() 
    {
        //Obtener el animator
        m_anim = GetComponent<Animator>();
    } 

    public void AddToEmptySpace(GameObject tool)
    {
        for (int i = 0; i < m_spaces.Count; i++)
        {
            if (m_spaces[i].transform.childCount == 0)
            {
                tool.transform.position = m_spaces[i].transform.position;
                tool.transform.rotation = m_spaces[i].transform.rotation;
                tool.transform.parent = m_spaces[i].transform;
                break;
            }
        }
    }

    public void RemoveFromSpace(GameObject tool)
    {
        for (int i = 0; i < m_spaces.Count; i++)
        {
            tool.transform.parent = null; 
        }
    }

    public int SpaceFree()
    {
        int num = 0;
        for (int i = 0; i < m_spaces.Count; i++)
        {
            if (m_spaces[i].transform.childCount == 0)
            {
                num++;
            }
        }

        Debug.Log(num);
        return num;
    }

    void OnEnable() => DH_Inventory.ActiveInventory += SuitcaseBehavior;

    void SuitcaseBehavior(bool active)
    {
        if (active) OpenSuitcase();
        else CloseSuitcase();
    }

    void Update()
    {
        //Camera -------------------

            //Initial position
            cameraInitialPos = new Vector3(0, 1.622f, 0);

        //------------------------

        //Transform -----------------------------

            //Initial Position;
            initialPosition = m_player.transform.position + m_player.transform.up * 1.5f + -m_player.transform.forward * 0.6f;

            //Initial Rotation
            initialRotation = Quaternion.LookRotation(m_player.transform.TransformDirection(Vector3.forward + new Vector3(0, 90, 0)));

        //-----------------------------------
    }

    float distance;
    void LateUpdate()
    {
        if (DH_GameManager.State == GameStates.UI && canOperate) Detect();
        else Cursor.SetCursor(m_default, Vector2.zero, CursorMode.Auto);
    }

    void Detect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, distance, m_layer))
        {
            if (hit.collider.CompareTag("DH_Tool"))
            {
                distance = hit.distance + 0.1f;
                Cursor.SetCursor(m_detecting, Vector2.zero, CursorMode.Auto);

                if (Input.GetKeyDown(KeyCode.Mouse0)) Tool?.Invoke(hit.collider.gameObject);
            }
            
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        }
        else
        {
            Cursor.SetCursor(m_default, Vector2.zero, CursorMode.Auto);
            distance = m_maxDistance;
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);
        }

        distance = Mathf.Clamp(distance, 0, m_maxDistance);
    }

    public void OpenSuitcase()
    {
        InSuitcase = true;
        Opening = true;

        //Camera ---------------------

            //Activación de la camara
            m_currentCamera = m_cameras[0];
            Invoke(nameof(CamerasBehavior), 0.2f);

        //-----------------

        StopAllCoroutines();
        StartCoroutine(PutSuitcaseInGround(true));
    }

    public void CloseSuitcase()
    {   
        Opening = true;

        operate = false;
        OperateBahevior();

        //Se reproduce la animación
        m_anim.CrossFade(m_closeAnimationName, 0.25f);

        m_currentCamera = m_cameras[0];
        Invoke(nameof(CamerasBehavior), 0);
        
        //Se invoca a la función que activará la corrutina
        Invoke(nameof(CloseSuitcaseCor), GetAnimationSeconds(m_anim, m_closeAnimationName) - 0.2f);
        
    }

    void CloseSuitcaseCor()
    {
        //Camera ---------------------

            //Desactivación de la camara
            m_currentCamera = null;
            Invoke(nameof(CamerasBehavior), 0);

        //---------------------------

        Opening = false;
        
        //Estado de juego -------------------

            //Se retoma el Gameplay
            DH_GameManager.State = GameStates.Gameplay;

        //---------------

        StopAllCoroutines();
        StartCoroutine(PutSuitcaseInGround(false));
    }

    void CamerasBehavior()
    {
        //Virtual Camera -------------------------

            //Desactivación de la cámara
            foreach (var item in m_cameras)
            {
                item.SetActive(false);

                if (item == m_currentCamera && item.activeSelf == false)
                {
                    item.SetActive(true);
                }
            }
            
        //---------------------
    }

    public float GetAnimationSeconds(Animator animator, string nameAnimation)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == nameAnimation)
            {
                return clip.length;
            }
        }

        Debug.LogWarning("No se encontró la animación con el nombre: " + nameAnimation);
        return 0f;
    }

    IEnumerator PutSuitcaseInGround(bool active)
    {
        //Suitcase -------------------------------------------------

            //Rotation ------------------------
            Quaternion initialRot = transform.rotation;
            Quaternion look = Quaternion.LookRotation(m_player.transform.TransformDirection(-Vector3.forward));
            Quaternion targetRot = active ? look : initialRotation;
            //---------------------------------

            //Position -----------------
            Vector3 forwardP = m_player.transform.position + m_player.transform.forward * forwardOffset;

            //Posiciones iniciales
            float xInitial = transform.position.x;
            float yInitial = transform.position.y;
            float zInitial = transform.position.z;

            //Nuevas posiciones
            float zTarget = active ? forwardP.z : initialPosition.z;
            float xTarget = active ? forwardP.x : initialPosition.x;
            float yTarget = active ? forwardP.y : initialPosition.y;
            //------------------------------

        // -------------------------------------------------------

        //Camara ------------------------

            Vector3 camInitialPosition = m_cameras[0].transform.localPosition;
            Vector3 cameraDesirePosition = active ? new Vector3(0, yTarget + 0.8f, 0) : cameraInitialPos;

        // ----------------------------------

        for (float i = 0; i < 1f; i+=Time.deltaTime)
        {
            //Time
            float t = i / 1f;

            //Camera ----------------------------

                m_cameras[0].transform.localPosition = Vector3.Lerp(camInitialPosition, cameraDesirePosition, t);

            // --------------------------------

            //Transform ------------------------------

                //Position
                float yIni = Mathf.Lerp(yInitial, yTarget, t);
                float xIni = Mathf.Lerp(xInitial, xTarget, t);
                float zIni = Mathf.Lerp(zInitial, zTarget, m_curve.Evaluate(t));
                transform.position = new Vector3(xIni, yIni, zIni);

                //Rotation
                transform.rotation = Quaternion.Slerp(initialRot, targetRot, t);

            // -----------------------------------------------
            
            //Yield
            yield return null;
        }

        //Camera -----------------------
            
            //Asiganción de la posición final
            m_cameras[0].transform.localPosition = cameraDesirePosition;

        //---------------------------

        //Transform ----------------------

            //Asiganción de la posición final
            transform.position = new Vector3(xTarget, yTarget, zTarget);

            //Asiganción de la rotación final
            transform.rotation = targetRot;

        // -----------------------------

        //Condiciones de animación y otras activaciones ---------------------

            //Animaciones
            if (yTarget == forwardP.y) 
            {
                m_anim.CrossFade(m_openAnimationName, 0.25f);
                Opening = false;

                m_currentCamera = m_cameras[1];
                Invoke(nameof(CamerasBehavior), GetAnimationSeconds(m_anim, m_openAnimationName) - 1.4f);

                operate = true;
                Invoke(nameof(OperateBahevior), GetAnimationSeconds(m_anim, m_openAnimationName) - 1.4f);
            }
            else
            {
                InSuitcase = false;
            }

        // ---------------------------------
    }

    bool operate;
    void OperateBahevior()
    {
        canOperate = operate;
        if (operate) 
        {
            if (m_inventory.m_toolInHand != null)
            {
                DH_UIManager.ActionState?.Invoke(DH_StateUI.InSuitcase, "Guardar objeto");
            }
        }
    }
}
