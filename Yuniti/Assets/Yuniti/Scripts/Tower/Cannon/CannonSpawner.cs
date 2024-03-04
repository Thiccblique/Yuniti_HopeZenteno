using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    public FollowPlayer followPlayer;
    public GameObject towerPrefab;
    public Transform playerPosition;

    public GameObject bigCollier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTower();
        if(RoundManager.instance.remainingEnemies <= 0 && followPlayer.stopReset == false)
        {
            Reset();
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
        followPlayer.stopDestroy = false;
    }
    IEnumerator CanonDestroy()
    {
        bigCollier.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        bigCollier.SetActive(false);
    }

}
