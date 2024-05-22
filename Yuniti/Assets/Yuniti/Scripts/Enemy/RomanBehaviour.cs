using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RomanBehaviour : EnemyBehaviourBase
{
    public static RomanBehaviour instance;


    public float coinsGiven = .7f;
    public NavMeshAgent enemyAgent;
    public GameObject waypoint;
    public GameObject[] spawnPoints;
    public float damageAmount = 0;
    public float attackRate = 0;

    public bool isPathingNeeded = false;

    //public int curHealth;

    private bool attackCooldown = false;
    private bool towerNearby = false;
    //private bool finishedPathing = false;
    private Vector3 destination;
    public Vector3 originalWaypointVector3;
    private Animator anim;
    //private Vector3 lastPoint;
    //private Vector3 currentAssignedPoint;
    //private bool reachedFirstPoint = false;
    //private int currentPathingIndex;
    private GameObject wpHit;
    private bool hasDecreasedEC = false;
    private bool enemyWPDC = false;

    private TowerHealth towerHealth;
    private PlayerHealth playerHealth;
    public PathManager pathManager;
    private ProjectileBehaviour projectileBehaviour;
    private Boom boom;
    private FWProjectileBehaviour fwProjectileBehaviour;
    private WallBehavior wallBehavior;
    public IsometricController player;
    public OrionController orionPlayer;


    public GameObject hitTextPrefab;
    public Transform hitPosition;

    [Header("Particals")]
    public ParticleSystem[] particleSystems;
    public GameObject death;

    [Header("EnemyBody")]
    public GameObject[] enemyVisuals;
    private NavMeshAgent navMesh;
    [Header("Health")]
    public float healthAmount;
    public float maxHealth;

    public Slider healthBar;


    void Start()
    {
        //currentPathingIndex = 0;
        if (waypoint != null)
        {
            originalWaypointVector3 = waypoint.transform.position; // This stores the waypoint of the assigned waypoint.
        }

        if (!isPathingNeeded)
        {
            destination = waypoint.transform.position; // This sets the Vector3 destination (X, Y, Z) for the Enemy to go to.
        }

        healthAmount = maxHealth;
        enemyAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPathingNeeded)
        {
            if (towerNearby == true)
            {
                enemyAgent.SetDestination(destination);
            }
            else
            {
                enemyAgent.SetDestination(originalWaypointVector3);
            }
        }

        if (healthAmount <= 0 && RoundManager.instance.roundNumber <= 9)
        {
            StartCoroutine(Death());
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
        if (other.gameObject.CompareTag("EnemyPathing"))
        {
            if (pathManager != null && enemyAgent != null)
            {
                wpHit = other.gameObject;
                
                if (!enemyWPDC)
                {
                    enemyWPDC = true;
                    enemyAgent.SetDestination(pathManager.getNextPoint(wpHit, this.gameObject).transform.position);
                    StartCoroutine(waypointCD());
                }
            }
        }

        if (other.gameObject.CompareTag("SpawnPoint"))
        {
            if (other.gameObject == spawnPoints[0])
            {
                enemyAgent.SetDestination(pathManager.startingPoints[0].transform.position);    
            }
            else if (other.gameObject == spawnPoints[1])
            {
                enemyAgent.SetDestination(pathManager.startingPoints[1].transform.position);
            }
        }
        

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

        if (other.gameObject.CompareTag("Bomb"))
        {
            boom = other.gameObject.GetComponent<Boom>();
            boom.StartEffectAndDeactivate();
            healthAmount = healthAmount / 2;
            healthAmount -= 5;

        }

        if (other.gameObject.CompareTag("HitBox"))
        {


            healthAmount -= player.attackDamage;

            ActivateRandomParticleSystem();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");

        }
        if (other.gameObject.CompareTag("SwordHitBox"))
        {
            healthAmount -= player.swordDamage;

            ActivateRandomParticleSystem();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");

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
        }

        if (other.gameObject.CompareTag("Player") && !attackCooldown && other.gameObject != null && orionPlayer.isMounted == false)
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.curHealth -= damageAmount;



            anim.SetBool("EnemyAttack", true);

            StartCoroutine(StartAttackCooldown());

            if (playerHealth.curHealth <= 0 && towerNearby == true)
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
            anim.SetBool("Hit", true);
            GameManager.instance.score += 4;
            //anim.SetBool("Hit", false);
            StartCoroutine(HitAnimReset());
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

    IEnumerator waypointCD()
    {
        yield return new WaitForSeconds(3);
        enemyWPDC = false;
    }

    IEnumerator TowerCheck()
    {
        yield return new WaitForSeconds(3);
        towerNearby = false;
    }

    IEnumerator HitAnimReset()
    {
        yield return new WaitForSeconds(0.01f);
        anim.SetBool("Hit", false);
    }

    IEnumerator Death()
    {
        death.SetActive(true);
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        navMesh.speed = 0;
        foreach (GameObject obj in enemyVisuals)
        {
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
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

    /* ENEMY HEALTH SYSTEM */

    public void SetMaxHealth(float health)
    {
        if (healthBar != null)
        {
            healthBar.maxValue = health;
            healthBar.value = health;
        }
    }
    public void SetHealth(float health)
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }
}
