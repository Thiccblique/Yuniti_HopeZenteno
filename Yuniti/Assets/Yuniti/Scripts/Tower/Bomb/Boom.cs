using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject boomParticle;
    public GameObject bombs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            bombs.SetActive(false);
        }

    }
    public void StartEffectAndDeactivate()
    {
        StartCoroutine(PlayEffectAndDeactivate());
    }

    IEnumerator PlayEffectAndDeactivate()
    {
        boomParticle.SetActive(true);
        yield return new WaitForSeconds(3f);
        bombs.SetActive(true);
        this.gameObject.SetActive(false);
        

    }
}
