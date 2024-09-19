using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tellory.UI.RingMenu
{
    public class RingLegacyInput : RingInputBase
    {
        // Structs
        [System.Serializable]
        public struct InputBehaviour
        {
            // Variables
            [SerializeField] private bool m_useMouseButton;
            [SerializeField] private int m_mouseButtonIndex;
            [SerializeField] private KeyCode m_keyboardKey;

            // Properties
            public readonly bool OnPress
            {
                get
                {
                    if (m_useMouseButton) return Input.GetMouseButtonDown(m_mouseButtonIndex);
                    else return Input.GetKeyDown(m_keyboardKey);
                }
            }

            public readonly bool OnRelease
            {
                get
                {
                    if (m_useMouseButton) return Input.GetMouseButtonUp(m_mouseButtonIndex);
                    else return Input.GetKeyUp(m_keyboardKey);
                }
            }
        }

        // Variables
        [Header("Legacy Input Behaviour")]
        [SerializeField] private InputBehaviour m_activationInput;
        [SerializeField] private InputBehaviour m_confirmationInput;

        // Methods
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            bool isActive = RingMenuManager.IsActive;

            // Confirmation
            if (isActive)
            {
                m_ringMenuManager.InputDistance = GetInputDistance();
                m_ringMenuManager.InputAngle = GetInputAngle();

                if (GetInputConfirm())
                {
                    m_ringMenuManager.TryConfirm();
                }
            }

            // Activation
            if (m_activationBehaviour == ActivationType.Hold)
            {
                if (GetInputPress())
                {
                    RefreshState(true);
                }

                if (GetInputRelease())
                {
                    RefreshState(false);
                }
            }
            else
            {
                if (GetInputPress())
                {
                    isActive = !isActive;
                    RefreshState(isActive);
                }
            }
        }

        private bool GetInputPress() => m_activationInput.OnPress;
        private bool GetInputRelease() => m_activationInput.OnRelease;
        private bool GetInputConfirm() => m_confirmationInput.OnPress;

        private float GetInputDistance()
        {
            Vector2 halfScreen;
            halfScreen.x = Screen.width / 2f;
            halfScreen.y = Screen.height / 2f;

            Vector2 mousePos = Input.mousePosition;

            return Vector2.Distance(halfScreen, mousePos);
        }

        private float GetInputAngle()
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2f;
            mousePos.y -= Screen.height / 2f;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if (angle < 0) angle = 180 + Mathf.Abs(-180 - angle);

            return angle;
        }
    }
}
