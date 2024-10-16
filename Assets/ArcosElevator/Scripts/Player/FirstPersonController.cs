﻿using UnityEngine;
using System.Collections;
using Cinemachine;

namespace ECM2
{
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Controller state")]
        [Tooltip("Can the player move, crouch and sprint?")]
        public bool canMove = true;
        [Tooltip("Can the player rotate the camera to look?")]
        public bool canLook = true;

        [Space(10.0f)]
        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow.")]
        public GameObject cameraTarget;
        [Tooltip("How far in degrees can you move the camera up.")]
        public float maxPitch = 80.0f;
        [Tooltip("How far in degrees can you move the camera down.")]
        public float minPitch = -80.0f;

        [Space(10.0f)]
        [Tooltip("FPS Cinemachine Virtual Camera")]
        public CinemachineVirtualCamera CMCamera;

        [Space(10.0f)]
        [Tooltip("Mouse look sensitivity")]
        public Vector2 lookSensitivity = new Vector2(1.5f, 1.25f);

        [Space(10.0f)]
        [Header("Crouch smoothing")]
        public float crouchDuration = 0.2f;
        public AnimationCurve crouchCurve;

        [Space(10.0f)]
        [Header("Sounds")]
        public float footstepInterval = 0.5f;
        public float footstepSprintInterval = 0.3f;
        public AudioClip[] SfxFootstepClips;
        public AudioClip SfxJumpClip;
        public AudioClip SfxLandedClip;
        private AudioSource _audioSource;
        private float _footstepTimer = 0f;
        private int lastFootstepIndex = -1;

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


        IEnumerator SmoothCrouch(bool active)
        {
            Vector3 initialCrouchPosition = cameraTarget.transform.localPosition;
            Vector3 targetCrouchPosition = active ? new Vector3(0, 1f, 0) : new Vector3(0, 1.65f, 0);

            for (float i = 0; i < crouchDuration; i += Time.deltaTime)
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
            _audioSource = GetComponent<AudioSource>();
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
            _character.Jumped += OnJumped;
            _character.Landed += OnLanded;

            // Subscribe to ring menu events

            RingMenuManager.OnActivated += OnRingMenuActivated;
            RingMenuManager.OnDeactivated += OnRingMenuDeactivated;
        }

        private void OnDisable()
        {
            // Unsubscribe to Character events

            _character.Crouched -= OnCrouched;
            _character.UnCrouched -= OnUnCrouched;
            _character.Jumped -= OnJumped;
            _character.Landed -= OnLanded;

            // Unsubscribe to ring menu events

            RingMenuManager.OnActivated += OnRingMenuActivated;
            RingMenuManager.OnDeactivated += OnRingMenuDeactivated;
        }

        private void OnRingMenuActivated()
        {
            Cursor.lockState = CursorLockMode.None;
            canLook = false;
        }

        private void OnRingMenuDeactivated()
        {
            Cursor.lockState = CursorLockMode.Locked;
            canLook = true;
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

        private void OnJumped()
        {
            _audioSource.PlayOneShot(SfxJumpClip);
        }

        private void OnLanded(Vector3 landingVelocity)
        {
            _audioSource.PlayOneShot(SfxLandedClip);
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

            if (canMove)
            {
                // Set Character movement direction

                _character.SetMovementDirection(movementDirection);

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

                // Sound effects
            }


            // Look input

            Vector2 lookInput;
            lookInput = new Vector2
            {
                x = Input.GetAxisRaw("Mouse X"),
                y = Input.GetAxisRaw("Mouse Y")
            };

            if (canLook)
            {
                // Add yaw input, this update character's yaw rotation

                AddControlYawInput(lookInput.x * lookSensitivity.x);

                // Add pitch input (look up / look down), this update cameraTarget's local rotation

                AddControlPitchInput(lookInput.y * lookSensitivity.y, minPitch, maxPitch);
            }

            if (_character.GetSpeed() > 0.5f && _character.IsOnGround())
            {

                _footstepTimer -= Time.deltaTime;

                if (_footstepTimer <= 0f)
                {
                    PlayFootstepSound();

                    _footstepTimer = _character.IsSprinting() ? footstepSprintInterval : footstepInterval;
                }

            }
            else
            {
                _footstepTimer = 0.2f;
            }
        }

        private void PlayFootstepSound()
        {
            int index = 0;

            if (SfxFootstepClips.Length > 1)
            {
                do
                {
                    index = Random.Range(0, SfxFootstepClips.Length);
                }
                while (index == lastFootstepIndex);
            }
            

            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(SfxFootstepClips[index]);

            lastFootstepIndex = index;
        }
    }
}