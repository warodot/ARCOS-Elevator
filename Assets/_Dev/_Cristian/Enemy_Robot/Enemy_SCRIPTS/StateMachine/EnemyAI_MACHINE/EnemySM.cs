using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySM : StateMachine
{
    [HideInInspector] public MovingState movingState;
    [HideInInspector] public SeekCover seekCoverState;
    [HideInInspector] public CoverState inCoverState;

    public NavMeshAgent agent;

    //Animation
    public Animator anim;

    //Enemy AI
    public Vector3 playerPos;
    public float movementRadius;
    public NavMeshObstacle agentObstacle;
    public float walkingSpeed, runningSpeed;

    //AI Combat
    public Quaternion currentRotation;
    public float rotationSpeed;
    public float timeToAttackCurrent, timeToAttackMax;
    public float currentAmmo, maxAmmo;
    public CombatState combatState;
    public float maxAttackCycles;
    public float timeBetweenShots;
    public int soldierType;
    public bool canAttack;

    //AI SFX
    public AudioSource gunAudioSource;
    public AudioClip gunshotClip;

    //Gun
    public GameObject flashSFX;
    public int weaponDamage;

    private void Awake()
    {
        seekCoverState = new SeekCover(this);
        movingState = new MovingState(this);
        inCoverState = new CoverState(this);

        currentAmmo = maxAmmo;
        timeToAttackCurrent = timeToAttackMax;
        combatState = CombatState.Idling;
    }

    protected override BaseState GetInitialState()
    {
        return seekCoverState;
    }

    public bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public enum CombatState
    {
        Idling,
        Fighting,
        Reloading,
        Cowering
    }


    public float GetAgentSpeed()
    {
        float speed = 0f; 
        speed = agent.velocity.magnitude / agent.speed;
        return speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, movementRadius);
    }

    public float WaitForAnimationToFinish(Animator anim, int indLayer)
    {
        return anim.GetCurrentAnimatorStateInfo(indLayer).length;
    }

    public void SendToConsole(float value, string animName)
    {
        Debug.Log(value);
    }
}

