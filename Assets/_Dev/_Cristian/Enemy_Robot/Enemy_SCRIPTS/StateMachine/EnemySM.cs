
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemySM : StateMachine
{
    //Movement States
    [HideInInspector] public SeekCoverState seekCoverState;
    [HideInInspector] public MoveToCoverState moveToCoverState;

    //Combat States
    [HideInInspector] public InCoverState inCoverState;
    [HideInInspector] public ShootingState shootingState;
    [HideInInspector] public ReloadingState reloadingState;

    //Tactics States
    [HideInInspector] public TacticsHubState tacticsHubState;

    //Grenade
    [HideInInspector] public GreanadeThrowState grenadeThrowState;


    //Suppresive Fire
    [HideInInspector] public SuppresiveFireState suppresiveFireState;

    //Flank
    [HideInInspector] public FlankingState flankingState;

    [Header("AI")]
    public NavMeshAgent agent;
    public List<NavMeshHit> storedHits = new();
    public EnemyState enemyState = EnemyState.Idle;
    public SoldierClass soldierClass = SoldierClass.Rifleman;

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
    public float weaponAccuracy;
    public int weaponDamage;
    public LayerMask whatIsEnemy;
    public NavMeshObstacle obstacle;

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
    public bool hasThrownGrenade;

    //SeekingCover
    public float seekingType = 0;

    //Instantiables
    public GameObject muzzleFlash;
    public Transform Projectile;
    public GameObject bulletHole;

    public AudioSource stepSFX;



    private void Awake()
    {
        seekCoverState = new SeekCoverState(this);
        moveToCoverState = new MoveToCoverState(this);
        inCoverState = new InCoverState(this);
        shootingState = new ShootingState(this);
        reloadingState = new ReloadingState(this);
        tacticsHubState = new TacticsHubState(this);
        grenadeThrowState = new GreanadeThrowState(this);
        suppresiveFireState = new SuppresiveFireState(this);
        flankingState = new FlankingState(this);

        currentAmmo = maxAmmo;
        timeToAttack = timeToAttackMaster;

        weaponAccuracy = soldierClass switch
        {
            SoldierClass.Rifleman => 70f,
            SoldierClass.MachineGunner => 60f,
            SoldierClass.Submachinegunner => 65f,
            _ => 70f,
        };
    }


    protected override BaseState GetInitialState()
    {
        return seekCoverState;
    }

    public enum SoldierClass
    {
        Rifleman,
        MachineGunner,
        Submachinegunner
    }

    public enum EnemyState
    {
        Idle,
        InCover,
        Moving,
        Attacking,
        Reloading
    }

    public void PlayStepSFX()
    {
        stepSFX.Play();
    }

    public async void CheckPlayerFlank()
    {
        await Task.Delay(500);
        if (!Physics.Linecast(transform.position, MapPlayerPosManager.instance.GetPlayerRef().transform.position))
        {
            Response();
        }

    }

    public void CheckPlayerDistance()
    {
        if (Vector3.Distance(transform.position, MapPlayerPosManager.instance.GetPlayerRef().transform.position) < 3f)
        {
            Response();
        }
    }

    void Response()
    {
        selectedTactic = 1;
        ChangeState(tacticsHubState);
    }

    public void Attack()
    {
        timeToAttack -= Time.deltaTime;
        if (attackCycle >= maxAttackCycle)
        {
            ChangeState(inCoverState);
        }
        if (currentAmmo == 0)
        {
            ChangeState(reloadingState);
        }
        if (timeToAttack < 0)
        {
            anim.SetTrigger("Attacking");
            FireRaycast();
            muzzleFlash.SetActive(true);
            weaponSource.PlayOneShot(firingSFX);
            timeToAttack = timeToAttackMaster;
            currentAmmo--;
            attackCycle++;
        }

    }

    public void FireRaycast()
    {
        if (Physics.Raycast(raycastSpawnPos.position, transform.forward, out RaycastHit hit, 200f, ~whatIsEnemy))
        {
            if (hit.transform.CompareTag("Player"))
            {
                var rand = Random.Range(0f, 100f);
                if (rand < weaponAccuracy)
                {
                    hit.collider.GetComponent<PlayerHealth>().TakeDamage(weaponDamage);
                }
                else
                {
                    hit.collider.GetComponentInChildren<BulletWhizzle>().FireSFX();
                }
            }
            else
            {
                if (hit.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.AddForceAtPosition(-hit.normal * 5f, hit.point, ForceMode.Impulse);
                }

                var offsetX = Random.Range(-0.3f, 0.3f);
                var offsetZ = Random.Range(-0.3f, 0.3f);
                var offsetY = Random.Range(-0.3f, 0.3f);
                var offsetPos = new Vector3(
                    x: hit.point.x + offsetX,
                    y: hit.point.y + offsetY,
                    z: hit.point.z + offsetZ);
                Instantiate(bulletHole, offsetPos, Quaternion.LookRotation(hit.normal, Vector3.up));
            }
        }
    }
    public Transform InstantiateGrenade()
    {
        return Instantiate(grenadeObj, grenadeSpawnPos.position, Quaternion.identity).transform;
    }

    public void RebakeNavmesh()
    {
        FindObjectOfType<NavMeshSurface>().BuildNavMesh();
    }

    public void SuppresiveFire()
    {
        timeToAttack -= Time.deltaTime;

        if (currentAmmo == 0)
        {
            ChangeState(reloadingState);
        }
        if (timeToAttack < 0)
        {
            anim.SetTrigger("Attacking");
            FireRaycast();
            weaponSource.PlayOneShot(firingSFX);
            timeToAttack = timeToAttackMaster;
            currentAmmo--;
        }
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
}