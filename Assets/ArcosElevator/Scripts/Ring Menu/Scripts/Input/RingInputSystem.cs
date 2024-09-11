using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Tellory.UI.RingMenu
{
    public class RingInputSystem : RingInputBase
    {

#if ENABLE_INPUT_SYSTEM

        // Variables
        [SerializeField] private InputActionReference m_pointerPosition;
        [SerializeField] private InputActionReference m_activation;
        [SerializeField] private InputActionReference m_confirmation;

        // Methods
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            m_pointerPosition.action.Enable();
            m_pointerPosition.action.started += OnPointerPositionChange;
            m_pointerPosition.action.performed += OnPointerPositionChange;
            m_pointerPosition.action.canceled += OnPointerPositionChange;

            m_activation.action.Enable();
            m_activation.action.started += OnActivationStart;
            m_activation.action.canceled += OnActivationCancel;

            m_confirmation.action.Enable();
            m_activation.action.canceled += OnConfirmationCancel;
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        private void OnDisable()
        {
            m_pointerPosition.action.Disable();
            m_pointerPosition.action.started -= OnPointerPositionChange;
            m_pointerPosition.action.performed -= OnPointerPositionChange;
            m_pointerPosition.action.canceled -= OnPointerPositionChange;

            m_activation.action.Disable();
            m_activation.action.started -= OnActivationStart;
            m_activation.action.canceled -= OnActivationCancel;

            m_confirmation.action.Disable();
            m_activation.action.canceled -= OnConfirmationCancel;
        }

        private void OnActivationStart(InputAction.CallbackContext context)
        {
            if (m_activationBehaviour == ActivationType.Hold) RefreshState(true);
            else RefreshState(!RingMenuManager.IsActive);
        }

        private void OnActivationCancel(InputAction.CallbackContext context)
        {
            if (m_activationBehaviour == ActivationType.Hold)
            {
                RefreshState(false);
            }
        }

        private void OnConfirmationCancel(InputAction.CallbackContext context)
        {
            if (!RingMenuManager.IsActive) return;
            m_ringMenuManager.TryConfirm();
        }

        private void OnPointerPositionChange(InputAction.CallbackContext context)
        {
            if (!RingMenuManager.IsActive) return;
            // #TODO: Mouse is in screen position and gamepad is normalized.
            // This is a fix using the player input control scheme.
            string currentControlScheme = PlayerInput.all[0].currentControlScheme;
            Debug.Log(currentControlScheme);

            bool isGamepad = currentControlScheme == "Gamepad";

            Vector2 pointerPosition = context.ReadValue<Vector2>();

            Vector2 halfScreen;
            halfScreen.x = Screen.width / 2f;
            halfScreen.y = Screen.height / 2f;

            // Distance
            if (isGamepad)
            {
                m_ringMenuManager.InputDistance = pointerPosition.magnitude * (m_ringMenuManager.MinimumDistance * 2f);
            }

            else
            {
                m_ringMenuManager.InputDistance = Vector2.Distance(halfScreen, pointerPosition);
            }


            // Angle
            Vector2 normalizedPointerPosition = pointerPosition;
            if (!isGamepad) normalizedPointerPosition -= halfScreen;

            float angle = Mathf.Atan2(normalizedPointerPosition.y, normalizedPointerPosition.x) * Mathf.Rad2Deg;
            if (angle < 0) angle = 180 + Mathf.Abs(-180 - angle);
            m_ringMenuManager.InputAngle = angle;
        }

#endif

    }
}