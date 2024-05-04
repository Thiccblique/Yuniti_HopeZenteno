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
            int randomPoint = Random.Range(0, 1);
            if (wpAvailable[randomPoint] == wpAvailable[0])
            {
                enemyBehaviour.isGoingClockwise = true;
            }
            else
            {
                enemyBehaviour.isGoingClockwise = false;
            }
            return wpAvailable[randomPoint];
        }
        else if (isDivergent && !enemyBehaviour.hasLooped && enemyBehaviour.hasHitFirstPoint)
        {
            enemyBehaviour.hasLooped = true;
            int randomPoint = Random.Range(0, 1);
            if (randomPoint == 0 && enemyBehaviour.isGoingClockwise)
            {
                return wpAvailable[0];
            }
            else if (randomPoint == 0 && !enemyBehaviour.isGoingClockwise)
            {
                return wpAvailable[1];
            }
            else
            {
                return wpAvailable[2];
            }

        }
        else if (isDivergent && enemyBehaviour.hasLooped)
        {
            return wpAvailable[2];
        }
        else if (wpHit == specialWaypoint[0] || wpHit == specialWaypoint[1])
        {
            if (!enemyBehaviour.hasHitSpecialPoint)
            {
                enemyBehaviour.hasHitSpecialPoint = true;
                return wpAvailable[0];
            }
            else
            {
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
                return wpAvailable[0];
            }
            else
            {
                return wpAvailable[1];
            }
        }
        
    }
}
