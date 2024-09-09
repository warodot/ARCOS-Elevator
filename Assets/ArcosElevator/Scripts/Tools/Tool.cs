using System.Collections;
using System.Collections.Generic;
using Tellory.UI.RingMenu;
using Unity.VisualScripting;
using UnityEngine;

//This is the base class for tools, inherit from this class to make a new tool.
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public abstract class Tool : MonoBehaviour
{
    public AudioClip equipSound;

    private AudioSource audioSource;
    private Animator animator;

    public class ToolFunction
    {
        public KeyCode activationKey;
        public System.Action method;
    }

    public List<ToolFunction> toolFunctions = new List<ToolFunction>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        PlaySound(equipSound);
    }

    // Helper method to create and add a ToolFunction
    protected void AddToolFunction(KeyCode key, System.Action action)
    {
        ToolFunction newFunction = new ToolFunction
        {
            activationKey = key,
            method = action
        };

        toolFunctions.Add(newFunction);
    }

    void PlaySound(AudioClip clip)
    {
        Debug.Log("playing sound");
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
            Debug.Log("attempted play");
        }
    }

    void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    private void Update()
    {
        foreach (var function in toolFunctions)
        {
            if (Input.GetKeyDown(function.activationKey))
            {
                function.method?.Invoke();
            }
        }
    }
}
