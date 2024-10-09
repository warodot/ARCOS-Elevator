using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreanadeThrowState : BaseState
{
    EnemySM _SM;

    public GreanadeThrowState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);
        _SM.Target = MapPlayerPosManager.instance.GetPlayerRef().transform;
        _SM.Projectile = GameObject.Instantiate(_SM.grenadeObj, _SM.grenadeSpawnPos.position, Quaternion.identity, _SM.grenadeSpawnPos).transform;

        // Move projectile to the position of throwing object + add some offset if needed.
        _SM.Projectile.position = _SM.transform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(_SM.Projectile.position, _SM.Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * _SM.firingAngle * Mathf.Deg2Rad) / _SM.gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(_SM.firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(_SM.firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        _SM.Projectile.rotation = Quaternion.LookRotation(_SM.Target.position - _SM.Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            _SM.Projectile.Translate(0, (Vy - (_SM.gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}