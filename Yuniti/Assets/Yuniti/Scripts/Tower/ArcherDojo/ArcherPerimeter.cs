using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPerimeter : MonoBehaviour
{
    private ArcherBehaviour archerBehaviour;
    private EnemyBehaviour enemyBehaviour;
    public LayerMask enemy;
    public LayerMask ally;
    private List<GameObject> targets = new List<GameObject>();
    private List<ArcherBehaviour> alliesInRange = new List<ArcherBehaviour>();
    private int maxTargets = 4;
    public int perimeterRange = 15;
    public GameObject[] patrolPoints;
    private int currentAllyIndex = 0;
    private bool shuffling = false;

    void Update()
    {
        if (targets.Count == 0 && alliesInRange.Count > 0 && !shuffling)
        {
            StartCoroutine(ShuffleAfterDelay());
        }
        else
        {
            Detect();
        }
    }

    void Patrol()
    {
        Collider[] allyColliders = Physics.OverlapSphere(transform.position, perimeterRange, ally);

        //alliesInRange.Clear();

        foreach (var collider in allyColliders) // for each ally collider detected in the OverlapSphere...
        {
            GameObject allyPawn = collider.gameObject; // Make an object out of the allyPawn...
            archerBehaviour = allyPawn.gameObject.GetComponent<ArcherBehaviour>(); // For each allyPawn we get their ArcherBehaviour component...
            if (archerBehaviour != null) // If the detected allyPawn has a an AllyBehaviour component then we added to the alliesInRange list...
            {
                alliesInRange.Add(archerBehaviour);
            }
        }

        ShufflePatrolPoints();

        int patrolPointsIndex = 0;

        for (int i = 0; i < alliesInRange.Count; i++)
        {
            GameObject point = patrolPoints[patrolPointsIndex];

            ArcherBehaviour archerBehavior = alliesInRange[i];
            archerBehavior.allyAgent.SetDestination(point.transform.position);

            patrolPointsIndex = (patrolPointsIndex + 1) % patrolPoints.Length;
        }
    }

    private IEnumerator ShuffleAfterDelay()
    {
        shuffling = true;
        yield return new WaitForSeconds(5f);
        Patrol();
        shuffling = false;
    }

    void Detect()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, perimeterRange, enemy); // Detects colliders in a layermask using OverlapSphere.
        Collider[] allyColliders = Physics.OverlapSphere(transform.position, perimeterRange, ally);

        //alliesInRange.Clear(); // Clears the alliesInRange list...
        targets.Clear(); // Clears the targets list...

        foreach (var collider in allyColliders) // for each ally collider detected in the OverlapSphere...
        {
            GameObject allyPawn = collider.gameObject; // Make an object out of the allyPawn...
            archerBehaviour = allyPawn.gameObject.GetComponent<ArcherBehaviour>(); // For each allyPawn we get their AllyBehaviour component...
            if (archerBehaviour != null) // If the detected allyPawn has a an AllyBehaviour component then we added to the alliesInRange list...
            {
                alliesInRange.Add(archerBehaviour);
            }
        }

        foreach (var collider in enemyColliders) // for each enemy collider detected in the OverlapSphere...
        {
            GameObject detectedEnemy = collider.gameObject; // a gameobject is created based on the enemy collider which allows the usage of gameobject methods.
            if (!targets.Contains(detectedEnemy) && targets.Count < maxTargets) // This adds enemies to the targets list depending if they weren't already added and the list has not reached max capacity.
            {
                targets.Add(detectedEnemy);
            }
        }

        ShuffleTargets(); // Randomizes the order of targets...

        for (int i = 0; i < Mathf.Min(targets.Count, alliesInRange.Count); i++) // For-Loop that makes sure that no extra targets are targeted if there are less targets than allies...
        {
            GameObject target = targets[i]; // GameObject target is set to whatever i variable value is in the targets array... This is randomized, so I can't say for sure...

            if (target == null) // If target doesn't exist... reset the path of all allies...
            {
                foreach (var archerBehavior in alliesInRange)
                {
                    archerBehavior.allyAgent.ResetPath();
                }
            }
            else // If target does exist... select random ally and set their transform to a randomized target...
            {
                ArcherBehaviour archerBehavior = alliesInRange[i];
                archerBehavior.allyAgent.SetDestination(target.transform.position);
            }
            // FURTHER COMMENTS:
            // The i is changing each iteration of this for-loop which makes both the ally and target/enemy random...
            // The void ShuffleTargets is what randomizes the target list which makes the list from both alliesInRange and targets not follow the same GameObjects...
            // For example, if we already had enemies and allies in these lists it would go from 0, 1, 2, etc...
            // The shuffle on the targets list would make it go so that the GameObject in slot 2 would go in slot 0 now...
            // So, now the ally in slot 0 will target the enemy that was previously in slot 2 and is now in slot 0...
            // The For-Loop will end once there are no enemies or no allies (the ally part is redundant since there are always allies in the perimeter if the player bought the ally spawner)...
        }
    }


    void ShuffleTargets() // This is something called a Fisher-Yates shuffle algorithm... I looked this one up...
    {
        for (int i = targets.Count - 1; i > 0; i--) // For-Loop that sets i as the last index available in the list and then subtracting by 1 each loop... This makes it go in reverse basically...
        {
            int randomIndex = Random.Range(0, i + 1); // Chooses a random index in the list that has yet to be chosen... Including the initial index chosen when the loop starts...
            GameObject temp = targets[i]; // Creates a temporary GameObject called temp depending on the GameObject at index i...
            targets[i] = targets[randomIndex]; // This makes it so the GameObject at index i is overwritten by whatever index is chosen by the Random.Range...
            targets[randomIndex] = temp; // Replaces the randomly selected index from before with the temporary GameObject we called earlier... This completes this round of the shuffle then loops for the rest of the list...
        }
    }

    public void ShufflePatrolPoints()
    {
        for (int i = patrolPoints.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = patrolPoints[i];
            patrolPoints[i] = patrolPoints[randomIndex];
            patrolPoints[randomIndex] = temp;
        }
    }
}
