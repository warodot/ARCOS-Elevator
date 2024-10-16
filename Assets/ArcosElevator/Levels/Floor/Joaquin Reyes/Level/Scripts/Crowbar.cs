using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour
{
    public Animator animator;
    public Vector3 lastPosition;
    public Transform player;
    public AudioSource attack;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, lastPosition) > 0.02f)
        {

            animator.SetBool("Walk", true);
        }
        else
        {

            animator.SetBool("Walk", false);

        }
        lastPosition = player.position;

        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Attack());
            
        }
    }

    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);

        yield return new WaitForSeconds(0.3f);

        attack.Play();
        animator.SetBool("Attack", false);
    }
}
