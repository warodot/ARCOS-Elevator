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
            _isOpen = !_isOpen;
            _targetRotation = _isOpen ? openedAngle : closedAngle;

            StopAllCoroutines();
            StartCoroutine(DoorBehavior(openingDuration, Quaternion.Euler(transform.eulerAngles.x, _targetRotation, transform.eulerAngles.z)));

            _audioSource.clip = _isOpen ? _doorOpenSFX : _doorStartClosingSFX;
            _audioSource.Play();
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
