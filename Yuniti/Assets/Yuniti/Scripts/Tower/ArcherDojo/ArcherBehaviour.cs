using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherBehaviour : MonoBehaviour
{
    public NavMeshAgent allyAgent;
    public int damageAmount = 1;
    public int attackRate = 1;
    private bool attackCooldown = false;
    private List<GameObject> targets = new List<GameObject>();
    private EnemyBehaviour enemyBehaviour;
    private ArcherPerimeter archerPerimeter;
    public float fireRate = 1f;
    public LayerMask enemy;
    public int maxTargets = 1;
    private float fireCountdown = 0f;
    public GameObject archerProjectile;
    public Transform shootingPoint;
    public float archerRange = 10f;


    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (fireCountdown <= 0.0f && RoundManager.instance.endRound == false)
        {
            Detect();
            fireCountdown = 1.0f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void OnDrawGizmosSelected() // This draws a red circle in scene view showing the tower range.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, archerRange);
    }

    void Detect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, archerRange, enemy); // Detects colliders in a layermask using OverlapSphere.
        foreach (var collider in hitColliders) // for each enemy collider detected in the OverlapSphere...
        {
            GameObject detectedEnemy = collider.gameObject; // a gameobject is created based on the enemy collider which allows the usage of gameobject methods.
            if (!targets.Contains(detectedEnemy) && targets.Count < maxTargets) // This adds enemies to the targets list depending if they weren't already added and the list has not reached max capacity.
            {
                targets.Add(detectedEnemy);
            }
        }

        foreach (var target in targets.ToArray()) // This shoots at each enemy inside the targets list.
        {
            if (target == null)
            {
                targets.Remove(target);
            }
            else
            {
                Shoot(target);
            }
        }
    }

    void Shoot(GameObject enemy)
    {
        GameObject newProjectile = Instantiate(archerProjectile, shootingPoint.position, Quaternion.identity); //Instantiates a projectile using a shooting position and the set rotation
        shootingPoint.transform.LookAt(enemy.transform); // This makes the shooting position to look at an enemy.
        transform.LookAt(enemy.transform);
        Vector3 direction = (enemy.transform.position - shootingPoint.position).normalized; // This calculates the position from the shooting point to the enemy
        newProjectile.GetComponent<Rigidbody>().AddForce(direction * 1000f); // We then use the direction here and add force to the instantiated projectile towards the enemy
    }
}
