using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();

        if (isOpen)
        {
            Open();
        }
    }


    public void Open()
    {
        isOpen = true;
        animator.SetBool("isOpen", true);
    }

    public void Close()
    {
        isOpen = false;
        animator.SetBool("isOpen", false);
    }

    public void ToggleOpen()
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.normalizedTime > 0.9f)
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
}
