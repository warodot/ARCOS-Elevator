using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tellory.UI.RingMenu
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RingMenuManager))]
    public abstract class RingInputBase : MonoBehaviour
    {
        // Enums
        public enum ActivationType { Hold, Toggle }

        // Variables
        [Header("Activation")]
        [SerializeField] protected ActivationType m_activationBehaviour;

        protected RingMenuManager m_ringMenuManager;

        //Añadido fácil 
        protected CanvasGroup m_canvasGroup;

        // Methods
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_ringMenuManager = GetComponent<RingMenuManager>();
            m_canvasGroup = GetComponent<CanvasGroup>(); //Añadido fácil
        }

        /// <summary>
        /// Refresh the ring menu manager state.
        /// </summary>
        protected void RefreshState(bool active)
        {
            if (active) m_ringMenuManager.TryOpen();
            else m_ringMenuManager.TryClose();
        }
    }
}
