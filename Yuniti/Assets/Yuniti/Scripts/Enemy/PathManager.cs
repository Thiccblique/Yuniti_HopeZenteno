using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;
    public GameObject[] startingPoints;
    public GameObject[] specialWaypoint;

    public GameObject getNextPoint(GameObject wpHit, GameObject curEnemy)
    {
        WPTracker wpT = wpHit.GetComponent<WPTracker>();
        EnemyBehaviourBase enemyBehaviour = null;

        if (curEnemy.GetComponent<RomanBehaviour>() != null)
        {
           enemyBehaviour = curEnemy.GetComponent<RomanBehaviour>();
        }
        else if (curEnemy.GetComponent<ColCatapultBehaviour>() != null)
        {
            enemyBehaviour = curEnemy.GetComponent<ColCatapultBehaviour>();
        }

        GameObject[] wpAvailable = wpT.waypointsAvailable;
        bool isDivergent = wpT.isDivergent;

        if (enemyBehaviour == null)
        {
            Debug.LogWarning("PathManager: EnemyBehaviour is null");
            return wpHit;
        }

        if (isDivergent && !enemyBehaviour.hasLooped && !enemyBehaviour.hasHitFirstPoint)
        {
            enemyBehaviour.hasHitFirstPoint = true;
            int randomPoint = Random.Range(0, 2);
            if (wpAvailable[randomPoint] == wpAvailable[0])
            {
                enemyBehaviour.isGoingClockwise = true;
            }
            else
            {
                enemyBehaviour.isGoingClockwise = false;
            }
            Debug.Log("isDiv, !hasLooped, !hasHitFirst");
            return wpAvailable[randomPoint];
        }
        else if (isDivergent && !enemyBehaviour.hasLooped && enemyBehaviour.hasHitFirstPoint)
        {
            enemyBehaviour.hasLooped = true;
            int randomPoint = Random.Range(0, 2);
            if (randomPoint == 0 && enemyBehaviour.isGoingClockwise)
            {
                Debug.Log("isDiv, !hasLooped, hasHitFirst");
                return wpAvailable[0];
            }
            else if (randomPoint == 0 && !enemyBehaviour.isGoingClockwise)
            {
                Debug.Log("isDiv, !hasLooped, hasHitFirst");
                return wpAvailable[1];
            }
            else
            {
                Debug.Log("isDiv, !hasLooped, hasHitFirst");
                return wpAvailable[2];
            }

        }
        else if (isDivergent && enemyBehaviour.hasLooped)
        {
            Debug.Log("isDiv, hasLooped");
            return wpAvailable[2];
        }
        else if (specialCheck(wpHit))
        {
            if (!enemyBehaviour.hasHitSpecialPoint)
            {
                Debug.Log("specialPoint");
                enemyBehaviour.hasHitSpecialPoint = true;
                return wpAvailable[0];
            }
            else
            {
                Debug.Log("specialPointReset");
                enemyBehaviour.hasHitSpecialPoint = false;
                enemyBehaviour.hasHitFirstPoint = false;
                enemyBehaviour.hasLooped = false;
                enemyBehaviour.isGoingClockwise = false;
                return wpAvailable[1];
            }
            
        }
        else
        {
            if (enemyBehaviour.isGoingClockwise)
            {
                Debug.Log("Clockwise");
                return wpAvailable[0];
            }
            else
            {
                Debug.Log("CounterClockwise");
                return wpAvailable[1];
            }
        }
        
    }

    public bool specialCheck(GameObject wpHit)
    {
        foreach (GameObject g in specialWaypoint)
        {
            if (g == wpHit)
            {
                return true;
            }
        }
        return false;
    }
}
