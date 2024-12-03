using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 15f;
    public float attackInterval = 1f;
    public Weapon turretWeapon;
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
            // Calculate the direction to the target
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f; // Keep the rotation on the horizontal plane

            // Create a rotation that faces the target
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            // Rotate the entire turret object around its original position (pivot point)
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
