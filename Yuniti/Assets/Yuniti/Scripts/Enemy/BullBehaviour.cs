using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BullBehaviour : MonoBehaviour
{
    public static BullBehaviour instance; // This is just used for other variables in other scripts to keep track of instance variables inside the instance of this BullBehaviour (if that makes sense).

    [Header("Bull Pathing")]
    public NavMeshAgent enemyBull; // makes a variable of type NavMeshAgent which used for the NavMesh navigating. We call it enemyBull.
    public GameObject[] pathing; // an array of GameObjects to get the Vector3 position.
    public GameObject target;

    private Vector3 lastPoint; // an instance variable (Vector3) that will get the last Vector3 position for the pathing array named lastPoint.
    private Vector3 currentAssignedPoint; // an instance variable (Vector3) that keeps track of the current Vector3 position the bull will be going towards to. Named currentAssignedPoint.
    public int currentPathingIndex; // an instance variable (int) that keeps track of the current index of the pathing array. For example, index 0 would be the first Vector3 waypoint. Named currentPathingIndex.

    private TowerHealth towerHealth;
    private PlayerHealth playerHealth;
    private ProjectileBehaviour projectileBehaviour;
    private FWProjectileBehaviour fwProjectileBehaviour;
    private WallBehavior wallBehavior;
    public IsometricController player;
    public OrionController orionPlayer;

    [Header("Health")]
    public float healthAmount;
    public float maxHealth;
    public Slider healthBar;

    [Header("Particals")]
    public ParticleSystem[] particleSystems;

    private string unTagged = "Untagged";
    public int newLayerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastPoint = pathing[pathing.Length - 1].transform.position; // assigns the lastPoint to the last GameObject's Vector3 position in the pathing array.
        currentAssignedPoint = pathing[0].transform.position; // assigns the first GameObject Vector3 position of the pathing array to currentAssignedPoint.
        currentPathingIndex = 0; // sets currentPathingIndex to 0 since in an array this would be the first element inside it.
        enemyBull.SetDestination(currentAssignedPoint); // sets the first destination using NavMesh to currentAssignedPoint which would be just the first Vector3 position of the first GameObject in pathing array.
        healthAmount = maxHealth;
        SetMaxHealth(maxHealth);
    }



    // Update is called once per frame
    void Update()
    {

        enemyBull.SetDestination(currentAssignedPoint); // constantly updates the destination of the bull using NavMesh based on the current Vector3 point assigned to currentAssignedPoint.
        SetHealth(healthAmount);

        if(RoundManager.instance.activateTower == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // === Bull Pathing ===

        if (other.gameObject.CompareTag("BullWaypoint") && other.gameObject.transform.position != lastPoint) // Checks the tag of the other object that has a collider trigger to see if it is "BullWaypoint".
        {                                                                                                       // Also checks if the other waypoint isn't the last point of the pathing array to avoid an IndexOutOfBounds error.
            currentPathingIndex++; // adds 1 to the currentPathingIndex variable
            currentAssignedPoint = pathing[currentPathingIndex].transform.position; // assigns the new Vector3 position of the next GameObject in the pathing array using the new currentPathingIndex number.   
        }

        if (other.gameObject.CompareTag("BullWaypoint") && other.gameObject.transform.position == lastPoint) // Checks the tag of the other object that has a collider trigger to see if it is "BullWaypoint".
        {                                                                                                    // It also checks if it is the last point in the pathing array to show that it has reached the final destination.
            // things to do once it reaches the last point
            RoundManager.instance.remainingEnemies--;
            
            Vector3 direction = target.transform.position - transform.position;

            // Use Quaternion.LookRotation to compute the rotation needed to look at the target
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Apply the rotation to the current GameObject
            transform.rotation = rotation;

            //Chages tag
            gameObject.tag = unTagged;
            gameObject.layer = newLayerIndex;

            //Destroy(other.gameObject);
        }

        // === Bull Being Attacked ===

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
            GameManager.instance.score += 99;
        }
        else
        {
            Debug.LogWarning("No particle systems found or added to the array!");
        }
    }
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
