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
    //private bool standingDown = true;
    private EnemyBehaviour enemyBehaviour;
    private SpiderBehaviour spiderBehaviour;
    private AllyPerimeter allyPerimeter;
  

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !attackCooldown && RoundManager.instance.endRound == false)
        {
            if (other.gameObject.GetComponent<EnemyBehaviour>() != null)
            {
                enemyBehaviour = other.gameObject.GetComponent<EnemyBehaviour>();
                enemyBehaviour.healthAmount = enemyBehaviour.healthAmount - damageAmount;
                Debug.Log("Enemy Health: " + enemyBehaviour.healthAmount);
            }
            else if (other.gameObject.GetComponent<SpiderBehaviour>() != null)
            {
                spiderBehaviour = other.gameObject.GetComponent<SpiderBehaviour>();
                spiderBehaviour.healthAmount = spiderBehaviour.healthAmount - damageAmount;
                Debug.Log("Enemy Health: " + spiderBehaviour.healthAmount);
            }


           StartCoroutine(StartAttackCooldown());
        }
    }

    IEnumerator StartAttackCooldown()
    {
        attackCooldown = true;
        yield return new WaitForSeconds(attackRate);
        attackCooldown = false;
    }
}
