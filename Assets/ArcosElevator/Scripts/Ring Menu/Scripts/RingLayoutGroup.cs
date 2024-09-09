using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Tellory.UI.RingMenu
{
    [ExecuteAlways]
    public class RingLayoutGroup : MonoBehaviour
    {
        // Variables
        [Header("Settings")]
        [Tooltip("Starting angle to put all the items on. 0 is right")]
        [SerializeField] private float m_initialAngle;

        [Tooltip("The distance in pixels from the center.")]
        [SerializeField] private float m_distance = 200;

        [Tooltip("Force expand to fill all the circle")]
        [SerializeField] private bool m_forceExpand = true;

        [Space]
        [Tooltip("Is clockwise? If is true, 90 is down, if not, 90 is up")]
        [SerializeField] private bool m_clockwise = true;

        [Tooltip("Separation in angles between items")]
        [SerializeField] private float m_separation = 20;

        [Header("References")]
        [SerializeField] private RingMenuItem m_itemPrefab;

        public Dictionary<int, RingMenuItem> Pool { get; private set; }

        // Methods
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (!Application.isPlaying) return;

            Pool = new Dictionary<int, RingMenuItem>();

            for (int i = 0; i < transform.childCount; i++)
            {
                // transform.GetChild(i).TryGetComponent(out RingMenuItem item);
                // Pool.Add(i, item);

                Destroy(transform.GetChild(i).gameObject);
            }

            RefreshLayout();
        }

#if UNITY_EDITOR

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            if (Application.isPlaying) return;
            EditorApplication.update += Update;
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        private void OnDisable()
        {
            if (Application.isPlaying) return;
            EditorApplication.update -= Update;
        }

        /// <summary>
        /// Called when the script is loaded or a value is changed in the
        /// inspector (Called in the editor only).
        /// </summary>
        private void OnValidate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector2 position = CalculatePosition(i, transform.childCount);
                (transform.GetChild(i) as RectTransform).anchoredPosition = position;
            }
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Application.isPlaying) return;

            for (int i = 0; i < transform.childCount; i++)
            {
                Vector2 position = CalculatePosition(i, transform.childCount);
                (transform.GetChild(i) as RectTransform).anchoredPosition = position;
            }
        }

#endif

        /// <summary>
        /// Refresh all the items.
        /// </summary>
        /// <param name="items"></param>
        public void RefreshLayout()
        {
            for (int i = 0; i < Pool.Count; i++)
            {
                if (Pool.TryGetValue(i, out var ringMenuItem))
                {
                    Vector2 position = CalculatePosition(i, Pool.Count);
                    ringMenuItem.SetPosition(position);
                }
            }
        }

        /// <summary>
        /// Refresh all the items.
        /// </summary>
        /// <param name="items"></param>
        public void ReplaceItems(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var ringMenuItem = GetItem(i);
                var item = items[i];

                Vector2 position = CalculatePosition(i, items.Count);

                ringMenuItem.Set(item, position);
            }
        }

        /// <summary>
        /// Get an item using the angle.
        /// </summary>
        public RingMenuItem GetItemOnAngle(float angle)
        {
            float separation;
            int count = Pool.Count;

            if (m_forceExpand) separation = 360f / count;
            else separation = m_separation;

            float direction = m_clockwise ? -1 : 1;

            separation *= direction;
            angle += m_initialAngle * direction;

            int index = 0;

            if (count > 0)
            {
                index = Mathf.RoundToInt(angle / separation) % count;
            }
            

            if (Pool.TryGetValue(index, out RingMenuItem item)) return item;
            else return null;
        }

        /// <summary>
        /// Get an item from the pool.
        /// If exists, returns it.
        /// If is null, replace with a new prefab.
        /// If doesn't exists, instantiate a new prefab.
        /// </summary>
        private RingMenuItem GetItem(int index)
        {
            RingMenuItem item;

            // The index exists in the dictionary
            if (Pool.TryGetValue(index, out item))
            {
                // Is not null, return this one.
                if (item != null) return item;

                // If is null, remove the current object in the index and create a new one from the prefab.
                Destroy(transform.GetChild(index).gameObject);

                item = Instantiate(m_itemPrefab, transform);
                item.transform.SetSiblingIndex(index);

                Pool[index] = item;

                return item;
            }

            // The item doesn't exists.
            else
            {
                item = Instantiate(m_itemPrefab, transform);
                item.transform.SetSiblingIndex(index);

                Pool.Add(index, item);

                return item;
            }
        }

        /// <summary>
        /// Returns the calculated position using serialized values, the index of the item and the total count.
        /// </summary>
        private Vector2 CalculatePosition(int index, int count)
        {
            float angle = m_initialAngle;
            float separation;

            if (m_forceExpand) separation = 360f / count * index;
            else separation = index * m_separation;

            angle += m_clockwise ? -separation : separation;
            float radian = angle * Mathf.Deg2Rad;

            Vector2 position;
            position.x = Mathf.Cos(radian) * m_distance;
            position.y = Mathf.Sin(radian) * m_distance;

            return position;
        }
    }
}
