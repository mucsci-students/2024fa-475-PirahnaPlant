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
    public bool disabled = true;

    public Transform verticalAimingPart;  // The part of the turret that moves vertically (aiming up/down)
    public float maxUpAngle = -17f; // Max angle for aiming up (negative for upwards)
    public float maxDownAngle = 20f; // Max angle for aiming down
    private float lastAttackTime = 0f;
    private PauseMenu pauseMenu;

    void Start(){
        GameObject menuManager = GameObject.Find("MenuManager"); // Assuming the object is named "MenuManager"
        if (menuManager != null)
        {
            pauseMenu = menuManager.GetComponent<PauseMenu>();
        }
    }
    void Update()
    {
        if (!disabled)
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
    }

    public void enableTurret()
    {
        disabled = false;
    }

    public void disableTurret()
    {
        disabled = true;
    }

    private void AimAtTarget()
    {
        if (target != null && !disabled)
        {
            // Calculate the direction to the target (horizontal)
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f; // Keep the rotation on the horizontal plane

            // Create a rotation that faces the target horizontally
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            // Rotate the entire turret object around its original position (pivot point)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 200f);

            // Now handle vertical aiming (up and down)
            AdjustVerticalAim(directionToTarget);

            // Rotate the shootSpot to match the vertical rotation of the verticalAimingPart
            AdjustShootSpotRotation();
        }
    }

    private void AdjustVerticalAim(Vector3 directionToTarget)
    {
        // Calculate the vertical offset (difference in height)
        float verticalOffset = target.position.y - transform.position.y;

        // Calculate the vertical angle in degrees (relative to horizontal distance)
        float angleToTarget = Mathf.Atan2(verticalOffset, directionToTarget.magnitude) * Mathf.Rad2Deg;

        // Invert the vertical angle logic: if the enemy is below, we want the turret to aim up (increasing the angle)
        // If the enemy is above, we want the turret to aim down (decreasing the angle).
        angleToTarget = -angleToTarget; // Flip the angle to reverse the behavior

        // Clamp the angle to be within the allowed up and down range
        angleToTarget = Mathf.Clamp(angleToTarget, maxUpAngle, maxDownAngle);

        // Apply the calculated vertical angle to the vertical aiming part of the turret
        if (verticalAimingPart != null)
        {
            verticalAimingPart.localRotation = Quaternion.Euler(angleToTarget, verticalAimingPart.localRotation.eulerAngles.y, verticalAimingPart.localRotation.eulerAngles.z);
        }
    }

    private void AdjustShootSpotRotation()
    {
        // Ensure shootSpot rotates the same way as the verticalAimingPart on the x-axis
        if (shootSpot != null && verticalAimingPart != null)
        {
            // Keep the shootSpot's y and z rotation the same as the verticalAimingPart
            float shootSpotRotationX = verticalAimingPart.localRotation.eulerAngles.x;
            float shootSpotRotationY = shootSpot.localRotation.eulerAngles.y;
            float shootSpotRotationZ = shootSpot.localRotation.eulerAngles.z;

            // Apply the vertical rotation of the verticalAimingPart to the shootSpot
            shootSpot.localRotation = Quaternion.Euler(shootSpotRotationX, shootSpotRotationY, shootSpotRotationZ);
        }
    }

    private void TryFireWeapon()
    {
        if (Time.time >= lastAttackTime + attackInterval && !disabled)
        {
            if (shootSpot != null)
            {
                turretWeapon.AIFiring();
            }

            lastAttackTime = Time.time;
        }
    }
}
