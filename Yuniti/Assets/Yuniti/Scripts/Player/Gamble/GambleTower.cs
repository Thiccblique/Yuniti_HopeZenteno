using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleTower : MonoBehaviour
{
    public float towerRange = 5f;
    public Transform shootingPoint;
    public LayerMask enemy;
    public GameObject towerProjectile;
    public float fireRate = 1f;
    public int towerDamage;

    private List<GameObject> targets = new List<GameObject>(); // Creates a list called targets to keep track of detected enemies.
    public int maxTargets = 1;
    private float fireCountdown = 0f;

    public int towerPrice = 2;

    private EnemyBehaviour enemyBehaviour;
    private ProjectileBehaviour projectileBehaviour;

  
    
    // Start is called before the first frame update
    void Start()
    {
        projectileBehaviour = towerProjectile.GetComponent<ProjectileBehaviour>();
        projectileBehaviour.damageAmount = towerDamage;
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
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }

    void Detect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, towerRange, enemy); // Detects colliders in a layermask using OverlapSphere.
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
        GameObject newProjectile = Instantiate(towerProjectile, shootingPoint.position, Quaternion.identity); //Instantiates a projectile using a shooting position and the set rotation
        shootingPoint.transform.LookAt(enemy.transform); // This makes the shooting position to look at an enemy. (In hindsight this doesn't do an)
        Vector3 direction = (enemy.transform.position - shootingPoint.position).normalized; // This calculates the position from the shooting point to the enemy
        newProjectile.GetComponent<Rigidbody>().AddForce(direction * 1000f); // We then use the direction here and add force to the instantiated projectile towards the enemy
    }
}
