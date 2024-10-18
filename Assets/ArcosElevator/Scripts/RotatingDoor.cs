using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Original code by Dheimid Solís.
/// Modified to fit the other current systems.
/// </summary>

public class RotatingDoor : MonoBehaviour, IInteractable
{
    [Header("Door settings")]
    public bool isRotationLocal = true;
    public bool startOpen = false;
    public float openedAngle = 90;
    public float closedAngle = 0;
    public float openingDuration = 1;
    public AnimationCurve animationCurve;

    [Header("Door unlocking")]
    public bool isLocked;
    public GameObject key;

    [Space(10.0f)]
    public UnityEvent OnTryOpenWhileLocked;
    public UnityEvent OnUnlocked;
    public UnityEvent OnOpened;
    public UnityEvent OnClosed;
    
    [Space(15.0f)]
    [Header("Door sounds")]
    [SerializeField] private AudioClip _doorOpenSFX;
    [SerializeField] private AudioClip _doorStartClosingSFX;
    [SerializeField] private AudioClip _doorFinishClosingSFX;
    [SerializeField] private AudioClip _doorLockedSFX;
    [SerializeField] private AudioClip _doorUnlockSFX;

    private float _targetRotation;
    private bool _isOpen;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }

    void Start()
    {
        if (startOpen)
        {
            _isOpen = true;
            
            if (isRotationLocal) transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, openedAngle, transform.eulerAngles.z);
            else transform.rotation = Quaternion.Euler(transform.eulerAngles.x, openedAngle, transform.eulerAngles.z);;
        }
    }

    public void Interact()
    {
        if (!isLocked || _isOpen)
        {
            if (_isOpen)
            {
                CloseDoor();
                OnClosed?.Invoke(); // Method CloseDoor doesn't call the event as to avoid stack overflow in case you want to sync doors
                // if event is needed, then call it manually.
            }
            else
            {
                OpenDoor();
                OnOpened?.Invoke(); // Same as the previous case.
            }
        } 
        else if(isLocked)
        {
            OnTryOpenWhileLocked?.Invoke();
            
            _audioSource.clip = _doorLockedSFX;
            _audioSource.Play();
        } 
        
    }

    public void LookedAt(){}

    public void LookedAway(){}

    public void UnlockDoor()
    {
        isLocked = false;

        _audioSource.PlayOneShot(_doorUnlockSFX);
        OnUnlocked?.Invoke();
    }
    
    public void OpenDoor()
    {
        _isOpen = true;
        _targetRotation = openedAngle;

        StopAllCoroutines();
        StartCoroutine(DoorBehavior(openingDuration, Quaternion.Euler(transform.eulerAngles.x, _targetRotation, transform.eulerAngles.z)));

        _audioSource.clip = _doorOpenSFX;
        _audioSource.Play();
    }

    public void CloseDoor()
    {
        _isOpen = false;
        _targetRotation = closedAngle;

        StopAllCoroutines();
        StartCoroutine(DoorBehavior(openingDuration, Quaternion.Euler(transform.eulerAngles.x, _targetRotation, transform.eulerAngles.z)));

        _audioSource.clip = _doorStartClosingSFX;
        _audioSource.Play();
    }


    IEnumerator DoorBehavior(float time, Quaternion targetRot)
    {
        Quaternion startRotation = isRotationLocal ? transform.localRotation : transform.rotation;
        Quaternion desiredRotation = targetRot;

        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float t = i / time;
            float curveValue = animationCurve.Evaluate(t);

            if (isRotationLocal) transform.localRotation = Quaternion.Slerp(startRotation, desiredRotation, curveValue);
            else transform.rotation = Quaternion.Slerp(startRotation, desiredRotation, curveValue);

            yield return null;
        }

        if (isRotationLocal) transform.localRotation = desiredRotation;
        else transform.rotation = desiredRotation;

        if (!_isOpen)
        {
            _audioSource.clip = _doorFinishClosingSFX;
            _audioSource.Play();
        }
    }


    public bool isOpen()
        { return _isOpen; }

    
}
