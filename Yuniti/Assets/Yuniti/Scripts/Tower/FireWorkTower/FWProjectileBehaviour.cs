using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWProjectileBehaviour : MonoBehaviour
{
    public LayerMask enemy;
    public float projectileLifetime = 5f;
    public int damageAmount = 1;
    public float explosionRange = 3f;
    public float speedMultiplier = 3f;

    public ParticleSystem fireWork;


    // Start is called before the first frame update
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 12, enemy); // Detects enemy colliders in a certain range using OverlapSphere.

        foreach (var collider in hitColliders) // for each enemy collider detected in the OverlapSphere...
        {
            GameObject enemy = collider.gameObject; // allows usage of GameObject methods (creates gameobject for enemies detected).
            transform.LookAt(enemy.transform.position); // Looks at the enemy
            transform.Rotate(0, 0, 0); // This then rotates -90 degrees in the x axis to give it a more arrow look.
        }
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseSpeed();
        projectileLifetime -= Time.deltaTime;

        if (projectileLifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") /*|| other.gameObject.CompareTag("Ground")*/)
        {
            SphereCollider explosionRadius = gameObject.GetComponent<SphereCollider>();
            explosionRadius.radius = explosionRange;
            //projectileLifetime = 0.05f;
            HitPartical();

        }
    }

    private void IncreaseSpeed()
    {
        // Get the current velocity of the object
        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;

        // Multiply the velocity by the speed multiplier
        Vector3 newVelocity = currentVelocity * speedMultiplier;

        // Set the new velocity to the object
        GetComponent<Rigidbody>().velocity = newVelocity;
    }
    private void HitPartical()
    {
        if (fireWork != null)
        {
            fireWork.gameObject.SetActive(true);
            fireWork.Play();
        }
    }
}
