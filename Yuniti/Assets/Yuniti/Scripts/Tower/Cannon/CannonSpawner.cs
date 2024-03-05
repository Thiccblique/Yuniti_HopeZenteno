using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    public FollowPlayer followPlayer;
    public GameObject towerPrefab;
    public Transform playerPosition;

    public GameObject bigCollier;

    public string tagToDestroy = "YourTagHere";
   

    // Update is called once per frame
    void Update()
    {
        SpawnTower();
        if(RoundManager.instance.remainingEnemies <= 0 && followPlayer.stopReset == false)
        {
            Reset();
            DestroyObjects();
            followPlayer.stopReset = true;
        }
    }

    void SpawnTower()
    {
        if (Input.GetKeyDown(KeyCode.X) && followPlayer.towercount > 0)
        {
            FindAnyObjectByType<AudioManager>().Play("Building");
            // Spawn the tower at the player's position
            Instantiate(towerPrefab, playerPosition.position, Quaternion.identity);
        }
        
    }
    private void Reset()
    {
        followPlayer.towercount = 3;
        StartCoroutine(CanonDestroy());
        foreach (GameObject obj in followPlayer.followers)
        {
            obj.SetActive(true);
        }
       
    }
    IEnumerator CanonDestroy()
    {
        bigCollier.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        bigCollier.SetActive(false);
    }

    public void DestroyObjects()
    {
        // Find all GameObjects with the specified tag
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tagToDestroy);

        // Loop through each object and destroy it
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
}
