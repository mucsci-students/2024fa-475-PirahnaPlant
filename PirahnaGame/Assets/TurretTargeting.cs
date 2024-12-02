using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 15f;
    public float attackInterval = 1f;
    public Weapon turretWeapon;
    public Transform turretModel;
    public Transform shootSpot;

    private float lastAttackTime = 0f;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        bool enemyInRange = false;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= detectionRange)
            {
                if (target == null || distanceToEnemy < Vector3.Distance(transform.position, target.position))
                {
                    target = enemy.transform;
                }

                enemyInRange = true;
            }
        }

        if (enemyInRange && target != null)
        {
            AimAtTarget();
            TryFireWeapon();
        }
        else
        {
            target = null;
        }
    }

    private void AimAtTarget()
    {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f;

            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 200f);
        }
    }

    private void TryFireWeapon()
    {
        if (Time.time >= lastAttackTime + attackInterval)
        {
            if (shootSpot != null)
            {
                turretWeapon.AIFiring();
            }

            lastAttackTime = Time.time;
        }
    }
}
