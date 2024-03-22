using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject teleportPoint; // The custom point to teleport the player to

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (teleportPoint != null)
        {
            player.transform.position = teleportPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("Teleport point is not assigned!");
        }
    }
}
