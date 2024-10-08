// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Unity.Mathematics;
// using UnityEngine;

// public class DH_InventoryBag : MonoBehaviour
// {
//     public Vector3 initialPosition;
//     public Vector3 finalPosition;
//     public float duration = 0.5f;
//     public List<GameObject> m_spaces;
//     public LayerMask m_layer;

//     [Space]
//     [Header("Choose Tool Related")]
//     public float m_maxDistance;
//     public Camera m_camera;
//     public Texture2D m_default;
//     public Texture2D m_detecting;


//     public void AddToSpace(GameObject tool)
//     {
//         for (int i = 0; i < m_spaces.Count; i++)
//         {
//             if (m_spaces[i].transform.childCount == 0)
//             {
//                 int layer = Mathf.RoundToInt(Mathf.Log(m_layer.value, 2));
                
//                 tool.gameObject.layer = layer;
//                 tool.transform.position = m_spaces[i].transform.position;
//                 tool.transform.rotation = m_spaces[i].transform.rotation;
//                 tool.transform.parent = m_spaces[i].transform;
//                 break;
//             }
//             else
//             {
//                 Debug.Log("no hay espacios disponibles");
//             }
//         }
//     }

//     public int SpaceFree()
//     {
//         int num = 0;
//         for (int i = 0; i < m_spaces.Count; i++)
//         {
//             if(m_spaces[i].transform.childCount == 0)
//             {
//                 num++;
//             }
//         }

//         Debug.Log(num);
//         return num;
//     }

//     public void RemoveToSpace(GameObject m_tool)
//     {
//         for (int i = 0; i < m_spaces.Count; i++)
//         {
//             if (m_spaces[i].transform.childCount > 0)
//             {
//                 if (m_spaces[i].transform.GetChild(0) == m_tool)
//                 {
//                     m_spaces[i].transform.GetChild(0).transform.parent = null;
//                     break;
//                 }
//             }
//         }
//     }

//     public void ShowBag()
//     {
//         gameObject.SetActive(true);
//         StopAllCoroutines();
//         StartCoroutine(BagBehavior(true));
//     }

//     public void HideBag()
//     {
//         StopAllCoroutines();
//         StartCoroutine(BagBehavior(false));
//     }

//     void OnEnable() => DH_Inventory.ActiveInventory += ActiveBag;

//     void ActiveBag(bool active)
//     {
//         if (active) ShowBag();
//         else HideBag();
//     }

//     IEnumerator BagBehavior(bool active)
//     {
//         Vector3 initialPos = transform.localPosition;
//         Vector3 targetPos = active ? finalPosition : initialPosition;

//         for (float i = 0; i < duration; i += Time.deltaTime)
//         {
//             float t = i / duration;
//             transform.localPosition = Vector3.Lerp(initialPos, targetPos, t);
//             yield return null;
//         }

//         transform.localPosition = targetPos; // Ajuste final
//         if (targetPos == initialPosition) gameObject.SetActive(false);
//     }

//     float distance;
//     void LateUpdate()
//     {
//         if (DH_GameManager.State == GameStates.UI) Detect();
//     }

//     void Detect()
//     {
//         Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
//         if (Physics.Raycast(ray, out RaycastHit hit, distance, m_layer))
//         {
//             if (hit.collider.CompareTag("DH_Tool"))
//             {
//                 distance = hit.distance;
//                 Cursor.SetCursor(m_detecting, Vector2.zero, CursorMode.Auto);

//                 if (Input.GetKeyDown(KeyCode.Mouse0)) Tool?.Invoke(hit.collider.gameObject);
//             }
            
//             Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
//         }
//         else
//         {
//             Cursor.SetCursor(m_default, Vector2.zero, CursorMode.Auto);
//             distance = m_maxDistance;
//             Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);
//         }

//         distance = Mathf.Clamp(distance, 0, m_maxDistance);
//     }
// }
