using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public static EnemyBehaviour instance;

    public NavMeshAgent enemyAgent;
    public GameObject waypoint;
    public int damageAmount = 0;
    public int attackRate = 0;
    public int healthAmount = 0;
    public int maxHealth = 3;
    private bool attackCooldown = false;
    private bool towerNearby = false;
    private Vector3 destination;
    public Vector3 originalWaypointVector3;

    private TowerHealth towerHealth;
    private ProjectileBehaviour projectileBehaviour;

    public Slider healthbar;
    public GameObject healthbarUI;

    void Start()
    {
        originalWaypointVector3 = waypoint.transform.position; // This stores the waypoint of the assigned waypoint.
        destination = waypoint.transform.position; // This sets the Vector3 destination (X, Y, Z) for the Enemy to go to.
        healthAmount = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (towerNearby == true)
        {
            enemyAgent.SetDestination(destination);
        }
        else
        {
            enemyAgent.SetDestination(originalWaypointVector3);
        }

        if (healthAmount <= 0)
        {
            healthAmount = 0;
            Destroy(gameObject);
            GameManager.instance.coins++;
            RoundManager.instance.remainingEnemies--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint")) // It detects if the collider trigger has the tag "Waypoint".
        {
            destination = other.transform.position; // This updates the destination as it collides with a collider.
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            projectileBehaviour = other.gameObject.GetComponent<ProjectileBehaviour>();
            healthAmount = healthAmount - projectileBehaviour.damageAmount;
            healthbar.value = CalculateHealth();
            Destroy(other.gameObject);
            Debug.Log("Enemy Health: " + healthAmount);
        }

        if (other.gameObject.CompareTag("HitBox"))
        {
            healthAmount--;
        }
    }

    float CalculateHealth()
    {
        return healthAmount;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint")) // It detects if the collider trigger has the tag "Waypoint".
        {
            towerNearby = true;
            StartCoroutine(TowerCheck());
        }

        if (other.gameObject.CompareTag("Tower") && !attackCooldown)
        {
            towerHealth = other.gameObject.GetComponent<TowerHealth>();
            towerHealth.curHealth = towerHealth.curHealth - damageAmount;
            Debug.Log("Tower Health: " + towerHealth.curHealth);

            StartCoroutine(StartAttackCooldown());

            if (towerHealth.curHealth <= 0 && towerNearby == true)
            {
                other.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator StartAttackCooldown()
    {
        attackCooldown = true;
        yield return new WaitForSeconds(attackRate);
        attackCooldown = false;
    }

    IEnumerator TowerCheck()
    {
        yield return new WaitForSeconds(3);
        towerNearby = false;
    }
}
