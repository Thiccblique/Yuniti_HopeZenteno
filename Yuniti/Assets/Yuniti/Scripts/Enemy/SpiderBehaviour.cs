using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SpiderBehaviour : MonoBehaviour
{
    public static SpiderBehaviour instance;

    public NavMeshAgent enemyAgent;
    public GameObject waypoint;
    public int damageAmount = 0;
    public int attackRate = 0;
    
    //public int curHealth;

    private bool attackCooldown = false;
    private bool towerNearby = false;
    private Vector3 destination;
    public Vector3 originalWaypointVector3;
    ///private Animator anim;

    private TowerHealth towerHealth;
    private ProjectileBehaviour projectileBehaviour;
    private FWProjectileBehaviour fwProjectileBehaviour;

  
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
        //anim = GetComponent<Animator>();
        SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        enemyAgent.SetDestination(originalWaypointVector3);
        if (healthAmount <= 0)
        {
            GameManager.instance.coins += .5f;
            RoundManager.instance.remainingEnemies--;
            healthAmount = 0;
            Destroy(gameObject);

        }
        SetHealth(healthAmount);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Projectile"))
        {
            projectileBehaviour = other.gameObject.GetComponent<ProjectileBehaviour>();
            healthAmount = healthAmount - projectileBehaviour.damageAmount;
           // healthbar.value = CalculateHealth();
            HitPartical();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");
            // Debug.Log("Enemy Health: " + healthAmount);
        }

        if (other.gameObject.CompareTag("FWProjectile"))
        {
            fwProjectileBehaviour = other.gameObject.GetComponent<FWProjectileBehaviour>();
            healthAmount = healthAmount - fwProjectileBehaviour.damageAmount;
           // healthbar.value = CalculateHealth();
            HitPartical();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");
            //Debug.Log("Enemy Health: " + healthAmount);
        }

        if (other.gameObject.CompareTag("HitBox"))
        {
            healthAmount--;
           // healthbar.value = CalculateHealth();
            HitPartical();
            FindAnyObjectByType<AudioManager>().Play("EnemyHit");
            //Debug.Log("Enemy Health: " + healthAmount);
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

        if (other.gameObject.CompareTag("Tower") && !attackCooldown && other.gameObject != null)
        {
            towerHealth = other.gameObject.GetComponent<TowerHealth>();
            towerHealth.curHealth -= damageAmount;
            Debug.Log("Tower Health: " + towerHealth.curHealth);


            //anim.SetBool("EnemyAttack", true);

            StartCoroutine(StartAttackCooldown());

            if (towerHealth.curHealth <= 0 && towerNearby == true)
            {
                other.gameObject.SetActive(false);
            }

        }

    }

    void CreateHitAnimation(Vector3 position)
    {
        GameObject hitText = Instantiate(hitTextPrefab, position, Quaternion.identity);
        HitTextAnimation hitTextAnimation = hitText.AddComponent<HitTextAnimation>();
    }

    private void HitPartical()
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
        //anim.SetBool("EnemyAttack", false);

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
