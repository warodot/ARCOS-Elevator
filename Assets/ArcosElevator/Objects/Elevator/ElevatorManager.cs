using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorManager : MonoBehaviour
{
    public static ElevatorManager Instance { get; private set; }

    public AudioClip sfx_ArrivingHumSound;
    public AudioClip sfx_LeavingHumSound;
    public AudioClip sfx_DoorsOpen;
    public AudioClip sfx_DoorsClose;

    public float CameraShakeIntensity = 0.04f;

    public bool areDoorsOpen = false;
    private bool areDoorsBusy = true;

    private AudioSource audioSource;
    private Animator animator;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        StartCoroutine(Arrive());
    }


    IEnumerator Arrive()
    {
        
        areDoorsBusy = true;
        audioSource.PlayOneShot(sfx_ArrivingHumSound);
        yield return new WaitForSeconds(sfx_ArrivingHumSound.length-2.4f);
        CinemachineShake.Instance.ShakeCamera(CameraShakeIntensity,0.8f);
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(OpenDoors());
    }

    IEnumerator OpenDoors()
    {
        audioSource.PlayOneShot(sfx_DoorsOpen);
        animator.Play("Elevator_Open");
        areDoorsBusy = true;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        areDoorsBusy = false;
        areDoorsOpen = true;
    }

    IEnumerator CloseDoors(bool leaveDoorsBusy)
    {
        audioSource.PlayOneShot(sfx_DoorsClose);
        animator.Play("Elevator_Close");
        areDoorsBusy = true;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        if (!leaveDoorsBusy)
        {
            areDoorsBusy = false;
        }
        areDoorsOpen = false;
    }

    IEnumerator Leave(string levelToLoad)
    {
        if (areDoorsOpen)
        {
            StartCoroutine(CloseDoors(true));
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animationDuration = stateInfo.length;

            yield return new WaitForSeconds(animationDuration);
        }

        CinemachineShake.Instance.ShakeCamera(CameraShakeIntensity,1f);
        audioSource.PlayOneShot(sfx_LeavingHumSound);
        yield return new WaitForSeconds(sfx_LeavingHumSound.length);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            Debug.Log(progressValue);
            yield return null;
        }
    }

    public void TryCloseDoors()
    {
        if (!areDoorsBusy && areDoorsOpen)
        {
            StartCoroutine(CloseDoors(false));
        }
    }

    public void TryOpenDoors()
    {
        if (!areDoorsBusy && !areDoorsOpen)
        {
            StartCoroutine(OpenDoors());
        }
    }

    public void StartChangeLevelRoutine(string newLevel)
    {
        StartCoroutine(Leave(newLevel));
    }
}
