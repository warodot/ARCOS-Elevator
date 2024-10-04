using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySM : StateMachine
{
    [HideInInspector] public SeekCoverState seekCoverState;
    [HideInInspector] public MoveToCoverState moveToCoverState;
    [HideInInspector] public InCoverState inCoverState;
    [HideInInspector] public ShootingState shootingState;

    [Header("AI")]
    public NavMeshAgent agent;
    public List<NavMeshHit> storedHits = new();
    public EnemyState enemyState = EnemyState.Idle;

    [Header("Cover Acquisition")]
    public float seekingIterations;
    public float turnRate;

    [Header("Combat")]
    public float timeToAttack;

    [Header("Animation Control")]
    public float currentSpeed;
    public Animator anim;

    [Header("Player References")]
    public LayerMask whatIsCover; 

    private void Awake()
    {
        seekCoverState = new SeekCoverState(this);
        moveToCoverState = new MoveToCoverState(this);
        inCoverState = new InCoverState(this);
        shootingState = new ShootingState(this);
    }

    public enum EnemyState
    {
        Idle,
        Moving,
        Attacking,
        Reloading
    }

    protected override BaseState GetInitialState()
    {
        return seekCoverState;
    }

    public void DrawDebugCircle(Vector3 center, float radius, Color color)
    {
        Vector3 prevPos = center + new Vector3(radius, 0, 0);
        for (int i = 0; i < 30; i++)
        {
            float angle = (float)(i + 1) / 30.0f * Mathf.PI * 2.0f;
            Vector3 newPos = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Debug.DrawLine(prevPos, newPos, color);
            prevPos = newPos;
        }
    }
}