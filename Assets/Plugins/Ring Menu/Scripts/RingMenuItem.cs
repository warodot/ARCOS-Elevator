using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tellory.UI.RingMenu
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Button))]
    public class RingMenuItem : MonoBehaviour
    {
        // Variables
        [Header("Interaction")]
        [SerializeField] private Item m_item;
        [SerializeField] private bool m_closeMenuOnPress;
        [Space]
        [SerializeField] private float m_hoverScale = 1.2f;
        [SerializeField] private float m_pressScale = 0.75f;

        [Header("References")]
        [SerializeField] private RectTransform m_rectTransform;
        [SerializeField] private Image m_iconRenderer;

        private RingMenuManager m_menuManager;

        // Methods
        /// <summary>
        /// Reset is called when the user hits the Reset button in the Inspector's
        /// context menu or when adding the component the first time.
        /// </summary>
        private void Reset()
        {
            m_rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            m_menuManager = GetComponentInParent<RingMenuManager>();
            if (m_item != null) SetItem(m_item);
        }

        /// <summary>
        /// Set the item position and values.
        /// </summary>
        public void Set(Item item, Vector2 position)
        {
            SetItem(item);
            SetPosition(position);
        }

        /// <summary>
        /// Set the item values.
        /// </summary>
        public void SetItem(Item item)
        {
            m_item = item;
            m_iconRenderer.sprite = item.Icon;
        }

        /// <summary>
        /// Set only the item position.
        /// </summary>
        public void SetPosition(Vector2 position)
        {
            m_rectTransform.anchoredPosition = position;
        }

        /// <summary>
        /// Trigger the interaction event.
        /// </summary>
        public void Interact()
        {
            if (m_item == null)
            {
                Debug.LogError($"Item with name: {gameObject.name} has not been initialized");
                return;
            }

            m_item.ExecuteAction();

            if (m_closeMenuOnPress)
            {
                if (m_menuManager) m_menuManager.TryClose();
                else Debug.LogError("Manager not found in parents, can't close on interact");
            }
        }

        /// <summary>
        /// Set the rect transform scale multiplying Vector3.one * scale.
        /// </summary>
        private void SetScale(float scale)
        {
            m_rectTransform.localScale = Vector3.one * scale;
        }

        /// <summary>
        /// Do a feedback when the pointer enters on this behaviour angle.
        /// </summary>
        public virtual void OnEnter()
        {
            SetScale(m_hoverScale);
        }

        /// <summary>
        /// Do a feedback when the pointer exit on this behaviour angle.
        /// </summary>
        public virtual void OnExit()
        {
            SetScale(1);
        }

        /// <summary>
        /// Trigger the event and do a feedback when the player confirms.
        /// </summary>
        public virtual void OnConfirm()
        {
            IEnumerator Release()
            {
                yield return new WaitForSecondsRealtime(0.2f);
                SetScale(1);
            }

            Interact();
            SetScale(m_pressScale);

            if (!m_closeMenuOnPress) StartCoroutine(Release());
        }
    }
}
