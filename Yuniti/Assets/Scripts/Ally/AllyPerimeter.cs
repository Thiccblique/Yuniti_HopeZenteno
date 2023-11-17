using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPerimeter : MonoBehaviour
{
    private AllyBehaviour allyBehaviour;
    private EnemyBehaviour enemyBehaviour;
    public LayerMask enemy;
    public LayerMask ally;
    private List<GameObject> targets = new List<GameObject>();
    private int maxTargets = 4;
    public int perimeterRange = 15;

    void Update()
    {
        Detect();
    }

    void Detect()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, perimeterRange, enemy); // Detects colliders in a layermask using OverlapSphere.
        Collider[] allyColliders = Physics.OverlapSphere(transform.position, perimeterRange, ally);
        foreach (var collider in allyColliders)
        {
            GameObject allyPawn = collider.gameObject;
            allyBehaviour = allyPawn.gameObject.GetComponent<AllyBehaviour>();
        }
        foreach (var collider in enemyColliders) // for each enemy collider detected in the OverlapSphere...
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
                allyBehaviour.allyAgent.ResetPath();
            }
            if (target != null)
            {
                allyBehaviour.allyAgent.SetDestination(target.transform.position);
            }
        }
    }
}
