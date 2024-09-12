using UnityEngine;
using System.Collections;
using Cinemachine;

namespace ECM2
{
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow.")]
        public GameObject cameraTarget;
        [Tooltip("How far in degrees can you move the camera up.")]
        public float maxPitch = 80.0f;
        [Tooltip("How far in degrees can you move the camera down.")]
        public float minPitch = -80.0f;

        [Space(15.0f)]
        [Tooltip("FPS Cinemachine Virtual Camera")]
        public CinemachineVirtualCamera CMCamera;

        [Space(15.0f)]
        [Tooltip("Mouse look sensitivity")]
        public Vector2 lookSensitivity = new Vector2(1.5f, 1.25f);

        [Space(15.0f)]
        [Header("Crouch smoothing")]
        public float crouchDuration = 0.2f;
        public AnimationCurve crouchCurve;

        // Cached Character

        private Character _character;

        // Current camera target pitch

        private float _cameraTargetPitch;

        /// <summary>
        /// Add input (affecting Yaw).
        /// This is applied to the Character's rotation.
        /// </summary>

        public void AddControlYawInput(float value)
        {
            _character.AddYawInput(value);
        }

        /// <summary>
        /// Add input (affecting Pitch).
        /// This is applied to the cameraTarget's local rotation.
        /// </summary>

        public void AddControlPitchInput(float value, float minValue = -80.0f, float maxValue = 80.0f)
        {
            if (value == 0.0f)
                return;

            _cameraTargetPitch = MathLib.ClampAngle(_cameraTargetPitch + value, minValue, maxValue);
            cameraTarget.transform.localRotation = Quaternion.Euler(-_cameraTargetPitch, 0.0f, 0.0f);
        }

        /// <summary>
        /// When character un-crouches, move camera target position offset.
        /// </summary>
        private void OnCrouched()
        {
            StopAllCoroutines();
            StartCoroutine(SmoothCrouch(true));
        }

        /// <summary>
        /// When character un-crouches, move camera target position offset.
        /// </summary>
        private void OnUnCrouched()
        {
            StopAllCoroutines();
            StartCoroutine(SmoothCrouch(false));
        }


        IEnumerator SmoothCrouch(bool active)
        {
            Vector3 initialCrouchPosition = cameraTarget.transform.localPosition;
            Vector3 targetCrouchPosition = active ? new Vector3(0, 1f, 0) : new Vector3(0, 1.65f, 0);

            for(float i = 0; i < crouchDuration; i += Time.deltaTime)
            {
                float t = i / crouchDuration;
                float curveValue = crouchCurve.Evaluate(t);

                cameraTarget.transform.localPosition = Vector3.Lerp(initialCrouchPosition, targetCrouchPosition, curveValue);
                yield return null;
            }

            cameraTarget.transform.localPosition = targetCrouchPosition;
        }

        private void Awake()
        {
            _character = GetComponent<Character>();
        }

        private void OnRingMenuActivated()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void OnRingMenuDeactivated()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void OnDestroy()
        {
            // Just in case, to avoid memory leak.
            RingMenuManager.OnActivated += OnRingMenuActivated;
            RingMenuManager.OnDeactivated += OnRingMenuDeactivated;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Disable Character's rotation mode, we'll handle it here

            _character.SetRotationMode(Character.RotationMode.None);
        }

        private void OnEnable()
        {
            // Subscribe to Character events

            _character.Crouched += OnCrouched;
            _character.UnCrouched += OnUnCrouched;

            // Subscribe to ring menu events

            RingMenuManager.OnActivated += OnRingMenuActivated;
            RingMenuManager.OnDeactivated += OnRingMenuDeactivated;
        }

        private void OnDisable()
        {
            // Unsubscribe to Character events

            _character.Crouched -= OnCrouched;
            _character.UnCrouched -= OnUnCrouched;

            // Unsubscribe to ring menu events

            RingMenuManager.OnActivated += OnRingMenuActivated;
            RingMenuManager.OnDeactivated += OnRingMenuDeactivated;
        }

        private void Update()
        {
            // Movement input

            Vector2 moveInput = new Vector2
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };

            // Movement direction relative to Character's forward

            Vector3 movementDirection = Vector3.zero;

            movementDirection += _character.GetRightVector() * moveInput.x;
            movementDirection += _character.GetForwardVector() * moveInput.y;

            // Set Character movement direction

            _character.SetMovementDirection(movementDirection);

            // Look input

            Vector2 lookInput;
            if (!RingMenuManager.IsActive)
            {
                lookInput = new Vector2
                {
                    x = Input.GetAxisRaw("Mouse X"),
                    y = Input.GetAxisRaw("Mouse Y")
                };
            }
            else
            {
                lookInput = Vector2.zero;
            }


            // Add yaw input, this update character's yaw rotation

            AddControlYawInput(lookInput.x * lookSensitivity.x);

            // Add pitch input (look up / look down), this update cameraTarget's local rotation

            AddControlPitchInput(lookInput.y * lookSensitivity.y, minPitch, maxPitch);

            // Crouch input

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
                _character.Crouch();
            else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
                _character.UnCrouch(); 

            // Sprint input
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
                _character.Sprint();
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                _character.StopSprinting();

            // Jump input

            if (Input.GetButtonDown("Jump"))
                _character.Jump();
            else if (Input.GetButtonUp("Jump"))
                _character.StopJumping();
        }
    }
}