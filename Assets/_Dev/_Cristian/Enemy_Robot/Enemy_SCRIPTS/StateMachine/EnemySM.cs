using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySM : StateMachine
{
    //Movement States
    [HideInInspector] public SeekCoverState seekCoverState;
    [HideInInspector] public MoveToCoverState moveToCoverState;

    //Combat States
    [HideInInspector] public InCoverState inCoverState;
    [HideInInspector] public ShootingState shootingState;
    [HideInInspector] public ReloadingState reloadingState;

    //Tactics State
    [HideInInspector] public TacticsHubState tacticsHubState;
    [HideInInspector] public GreanadeThrowState grenadeThrowState;

    [Header("AI")]
    public NavMeshAgent agent;
    public List<NavMeshHit> storedHits = new();
    public EnemyState enemyState = EnemyState.Idle;
    public SoldierClass soldierClass = SoldierClass.Rifleman;
    public MentalState mentalState = MentalState.Fresh;

    [Header("Cover Acquisition")]
    public float seekingIterations;
    public float turnRate;

    [Header("Combat")]
    public float switchToAttackTime;
    public float timeToAttack, timeToAttackMaster;
    public float currentAmmo;
    public float maxAmmo;
    public float attackCycle, maxAttackCycle;
    public Transform raycastSpawnPos;

    [Header("Audio")]
    public AudioSource weaponSource;
    public AudioClip firingSFX, reloadingSFX;

    [Header("Animation Control")]
    public float currentSpeed;
    public Animator anim;

    [Header("Player References")]
    public LayerMask whatIsCover;

    [Header("Tactics Manager")]
    public int selectedTactic;
    public int givenRole;

    [Header("Grenade Throw")]
    public Transform grenadeSpawnPos;
    public GameObject grenadeObj;
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;

    private void OnEnable()
    {
        EnemiesManager.instance.AddEnemy(this);
    }
    private void Awake()
    {
        seekCoverState = new SeekCoverState(this);
        moveToCoverState = new MoveToCoverState(this);
        inCoverState = new InCoverState(this);
        shootingState = new ShootingState(this);
        reloadingState = new ReloadingState(this);
        tacticsHubState = new TacticsHubState(this);
        grenadeThrowState = new GreanadeThrowState(this);

        currentAmmo = maxAmmo;
        timeToAttack = timeToAttackMaster;
    }

    public enum SoldierClass
    {
        Rifleman,
        MachineGunner,
        Submachinegunner
    }

    public enum MentalState
    {
        Fresh,
        Concerned,
        Scared,
        Cowering,
        Fanatical
    }

    public enum EnemyState
    {
        Idle,
        InCover,
        Moving,
        Attacking,
        Reloading
    }

    void HandleMentalState()
    {

    }

    protected override BaseState GetInitialState()
    {
        return seekCoverState;
    }


    public void Turn()
    {
        var towardsPlayer = MapPlayerPosManager.instance.GetPlayerRef().transform.position - transform.position;
        towardsPlayer.y = 0;
        transform.rotation = Quaternion.RotateTowards(
            from: transform.rotation,
            to: Quaternion.LookRotation(towardsPlayer),
            maxDegreesDelta: Time.deltaTime * turnRate
        );
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