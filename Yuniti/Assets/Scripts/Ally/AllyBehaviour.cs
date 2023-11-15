using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyBehaviour : MonoBehaviour
{
    public NavMeshAgent allyAgent;
    public int damageAmount = 1;
    public int attackRate = 1;
    private bool attackCooldown = false;
    private EnemyBehaviour enemyBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyBehaviour = other.gameObject.GetComponent<EnemyBehaviour>();
            Vector3 enemyTransform = other.transform.position;
            allyAgent.SetDestination(enemyTransform);

            if (!attackCooldown)
            {
                enemyBehaviour.healthAmount = enemyBehaviour.healthAmount - damageAmount;
                Debug.Log("Enemy Health: " + enemyBehaviour.healthAmount);

                StartCoroutine(StartAttackCooldown());
            } 
        }
    }

    IEnumerator StartAttackCooldown()
    {
        attackCooldown = true;
        yield return new WaitForSeconds(attackRate);
        attackCooldown = false;
    }
}
