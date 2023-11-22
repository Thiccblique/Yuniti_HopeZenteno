using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform firePoint; 
    public GameObject projectilePrefab; 
    public LayerMask enemyLayer; 
    public float shootingRange = 5f; 
    public float fireRate = 2f;

    private float fireCountdown = 0.0f;

    void Update()
    {
        if (fireCountdown <= 0.0f)
        {
            DetectAndShoot();
            fireCountdown = 1.0f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void DetectAndShoot()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRange, enemyLayer);

        foreach (var collider in hitColliders)
        {
            GameObject enemy = collider.gameObject;
            // Shoot at the detected enemy
            Shoot(enemy);
        }
    }

    void Shoot(GameObject enemy)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        // Face the projectile towards the enemy
        newProjectile.transform.LookAt(enemy.transform);
        // Add force to the projectile (adjust the force as needed)
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * 500f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
