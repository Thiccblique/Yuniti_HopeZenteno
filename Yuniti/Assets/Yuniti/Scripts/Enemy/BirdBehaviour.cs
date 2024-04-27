using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BirdBehaviour : MonoBehaviour
{
    public static BirdBehaviour instance;

    public float coinsGiven = .7f;
    public NavMeshAgent enemyAgent;
    public GameObject waypoint;
    public float damageAmount = 0;
    public float attackRate = 0;

    //public int curHealth;

    private bool attackCooldown = false;
    private bool towerNearby = false;
    private Vector3 destination;
    public Vector3 originalWaypointVector3;
    private Animator anim;
    private bool hasDecreasedEC = false;

    private TowerHealth towerHealth;
    private ProjectileBehaviour projectileBehaviour;
    private FWProjectileBehaviour fwProjectileBehaviour;
    private WallBehavior wallBehavior;
    public IsometricController player;


    public GameObject hitTextPrefab;
    public Transform hitPosition;

    [Header("Particals")]
    public ParticleSystem[] particleSystems;

    [Header("Health")]
    public float healthAmount;
    public float maxHealth;

    public Slider healthBar;


    void Start()
    {
        originalWaypointVector3 = waypoint.transform.position; // This stores the waypoint of the assigned waypoint.
        destination = waypoint.transform.position; // This sets the Vector3 destination (X, Y, Z) for the Enemy to go to.
        healthAmount = maxHealth;
        anim = GetComponent<Animator>();
        SetMaxHealth(maxHealth);
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

        if (healthAmount <= 0 && RoundManager.instance.roundNumber <= 9)
        {
            GameManager.instance.coins++;
            GameManager.instance.score += 99;
            GameManager.instance.killCount++;
            if (!hasDecreasedEC)
            {
                RoundManager.instance.remainingEnemies--;
                hasDecreasedEC = true;
            }
            healthAmount = 0;
            Destroy(gameObject);

        }
        else if (healthAmount <= 0)
        {
            GameManager.instance.coins++;
            healthAmount = 0;
            Destroy(gameObject);
        }
        SetHealth(healthAmount);
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
            // healthBar.value = CalculateHealth();
            //HitPartical();
            ActivateRandomParticleSystem();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");
            // Debug.Log("Enemy Health: " + healthAmount);
        }

        if (other.gameObject.CompareTag("FWProjectile"))
        {
            fwProjectileBehaviour = other.gameObject.GetComponent<FWProjectileBehaviour>();
            healthAmount = healthAmount - fwProjectileBehaviour.damageAmount;
            //healthBar.value = CalculateHealth();
            //HitPartical();
            ActivateRandomParticleSystem();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");
            //Debug.Log("Enemy Health: " + healthAmount);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            // wallBehavior = other.gameObject.GetComponent<WallBehavior>();
            //healthAmount = healthAmount - wallBehavior.damageAmount;
            healthAmount--;
            //healthbar.value = CalculateHealth();
            //HitPartical();
            ActivateRandomParticleSystem();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");
            // Debug.Log("Enemy Health: " + healthAmount);
        }

       
        
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint")) // It detects if the collider trigger has the tag "Waypoint".
        {
            towerNearby = true;
            StartCoroutine(TowerCheck());
        }

        if (other.gameObject.CompareTag("Tower") && !attackCooldown && other.gameObject != null)
        {
            towerHealth = other.gameObject.GetComponent<TowerHealth>();
            towerHealth.curHealth -= damageAmount;
            Debug.Log("Tower Health: " + towerHealth.curHealth);


            anim.SetBool("EnemyAttack", true);

            StartCoroutine(StartAttackCooldown());

            if (towerHealth.curHealth <= 0 && towerNearby == true)
            {
                other.gameObject.SetActive(false);
            }

        }

    }


    public void ActivateRandomParticleSystem()
    {
        if (particleSystems != null && particleSystems.Length > 0)
        {
            int randomIndex = Random.Range(0, particleSystems.Length);

            // Disable all particle systems in the array
            for (int i = 0; i < particleSystems.Length; i++)
            {
                if (particleSystems[i].isPlaying)
                {
                    particleSystems[i].Stop();
                }
            }

            // Activate the randomly selected particle system
            particleSystems[randomIndex].Play();
            GameManager.instance.score += 4;
        }
        else
        {
            Debug.LogWarning("No particle systems found or added to the array!");
        }
    }

    IEnumerator StartAttackCooldown()
    {
        attackCooldown = true;
        yield return new WaitForSeconds(attackRate);
        attackCooldown = false;
        anim.SetBool("EnemyAttack", false);

    }

    IEnumerator TowerCheck()
    {
        yield return new WaitForSeconds(3);
        towerNearby = false;
    }

    /* ENEMY HEALTH SYSTEM */

    public void SetMaxHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
}
