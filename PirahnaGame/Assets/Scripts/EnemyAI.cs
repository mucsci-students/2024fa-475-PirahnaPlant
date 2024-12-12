using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    public float obstacleAvoidanceRadius = 1.5f;
    public float damage = 10f;
    private Transform target;
    private float attackCooldown = 1.5f;
    private float attackTimer = 0f;
    public bool suicidal;

    private Animator anim;
    private int whoTarget;

    private void Start()
    {
        // Generate a random value 1 or 0 
        // If 0 enemy will target core no matter what
        // If 1 enemy will target closest turret or player
        whoTarget = Random.Range(0, 2);
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("Moving", true);
        }
    }

    void Update()
    {
        UpdateTarget();

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Collider[] obstacles = Physics.OverlapSphere(transform.position, obstacleAvoidanceRadius);
            foreach (var obstacle in obstacles)
            {
                if (obstacle.gameObject != gameObject)
                {
                    Vector3 avoidDirection = transform.position - obstacle.transform.position;
                    direction += avoidDirection.normalized * 0.5f;
                }
            }

            transform.position += direction.normalized * speed * Time.deltaTime;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }

    private void UpdateTarget()
    {
        // Enemy will only target core
        if (whoTarget == 0)
        {
            GameObject core = GameObject.FindGameObjectWithTag("Core");
            if (core != null)
            {
                target = core.transform;
                return;
            }
        }
        
        GameObject player = FindClosestObjectWithTag("Player");
        GameObject turret = FindClosestObjectWithTag("Turret");

        if (player != null && turret != null)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            float turretDistance = Vector3.Distance(transform.position, turret.transform.position);

            target = playerDistance < turretDistance ? player.transform : turret.transform;
        }
        else if (player != null)
        {
            target = player.transform;
        }
        else if (turret != null)
        {
            target = turret.transform;
        }
        else
        {
            target = null;
        }
    }

    private GameObject FindClosestObjectWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closest = obj;
                closestDistance = distance;
            }
        }

        return closest;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Turret") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Core"))
        {
            Health healthScript = collision.gameObject.GetComponent<Health>();
            anim.SetBool("Moving", false);
            anim.SetBool("Attacking", true);
            if (suicidal)
            {
                Health script = gameObject.GetComponent<Health>();
                float health = script.getCurrentHealth();
                script.ChangeHealth(-health);
            }
            if (healthScript != null)
            {
                if (attackTimer > 0f)
                {
                    attackTimer -= Time.deltaTime;
                } else
                {
                    if (collision.gameObject.CompareTag("Core"))
                    {
                        healthScript.CoreDamage(-damage);
                        attackTimer = attackCooldown;
                    } else if (collision.gameObject.CompareTag("Turret"))
                    {
                        healthScript.TurretDamage(-damage);
                    }
                    {
                        healthScript.ChangeHealth(-damage);
                        attackTimer = attackCooldown;
                    }
                    
                }
                  
            }

            //Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        anim.SetBool("Attacking", false);
        anim.SetBool("Moving", true);
    }
}
