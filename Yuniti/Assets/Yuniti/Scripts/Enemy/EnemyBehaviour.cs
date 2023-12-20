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
    public int damageAmount;
    public int attackRate = 0;
    public int healthAmount;
    public int maxHealth;
    public int curHealth;
    
    private bool attackCooldown = false;
    private bool towerNearby = false;
    private Vector3 destination;
    public Vector3 originalWaypointVector3;
    private Animator anim;

    private TowerHealth towerHealth;
    private ProjectileBehaviour projectileBehaviour;
    private FWProjectileBehaviour fwProjectileBehaviour;

    public Slider healthbar;
    public GameObject healthbarUI;
    public GameObject hitTextPrefab;
    public Transform hitPosition;

    [Header("Particals")]
    public ParticleSystem hitPartical;


    void Start()
    {
        originalWaypointVector3 = waypoint.transform.position; // This stores the waypoint of the assigned waypoint.
        destination = waypoint.transform.position; // This sets the Vector3 destination (X, Y, Z) for the Enemy to go to.
        healthAmount = maxHealth;
        anim = GetComponent<Animator>();
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
            GameManager.instance.coins++;
            RoundManager.instance.remainingEnemies--;
            healthAmount = 0;
            Destroy(gameObject);
            
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
            HitPartical();
            // Debug.Log("Enemy Health: " + healthAmount);
        }

        if (other.gameObject.CompareTag("FWProjectile"))
        {
            fwProjectileBehaviour = other.gameObject.GetComponent<FWProjectileBehaviour>();
            healthAmount = healthAmount - fwProjectileBehaviour.damageAmount;
            healthbar.value = CalculateHealth();
            HitPartical();
            //Debug.Log("Enemy Health: " + healthAmount);
        }

        if (other.gameObject.CompareTag("HitBox"))
        {
            healthAmount--;
            healthbar.value = CalculateHealth();
            HitPartical();
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

        if (other.gameObject.CompareTag("Tower") && !attackCooldown)
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

    void CreateHitAnimation(Vector3 position)
    {
        GameObject hitText = Instantiate(hitTextPrefab, position, Quaternion.identity);
        HitTextAnimation hitTextAnimation = hitText.AddComponent<HitTextAnimation>();
    }

    private void HitPartical()
    {
        if (hitPartical != null)
        {
            hitPartical.gameObject.SetActive(true);
            hitPartical.Play();
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
}
