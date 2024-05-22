using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public LayerMask enemy;
    public float projectileLifetime = 5f;
    public int damageAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 12, enemy); // Detects enemy colliders in a certain range using OverlapSphere.

        foreach (var collider in hitColliders) // for each enemy collider detected in the OverlapSphere...
        {
            GameObject enemy = collider.gameObject; // allows usage of GameObject methods (creates gameobject for enemies detected).
            transform.LookAt(enemy.transform.position); // Looks at the enemy 
            transform.Rotate(-90, 0, 0); // This then rotates -90 degrees in the x axis to give it a more arrow look.
        }
    }

    // Update is called once per frame
    void Update()
    {
        projectileLifetime -= Time.deltaTime;

        if (projectileLifetime < 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")/*|| other.gameObject.CompareTag("Ground")*/)
        {
            projectileLifetime = 0.05f;
        }
    }
}
