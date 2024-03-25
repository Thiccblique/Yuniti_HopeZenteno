using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsActivator : MonoBehaviour
{
    public TutorialStats tutorialStats;
    public TowerSpawner defendingPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (defendingPoints.spawnOne == true && FindAnyObjectByType<GameManager>().inRound == false)
        {
            tutorialStats.startedHud = true;
        }

    }
}
