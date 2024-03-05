using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDespawner : MonoBehaviour
{
    public FollowPlayer followPlayer;
    //public ParticleSystem goPoof;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(RoundManager.instance.remainingEnemies <= 0 && followPlayer.stopDestroy == false)
        {
            StartCoroutine(Destroy());
            followPlayer.stopDestroy = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CannonDestroyer"))
        {
            Destroy(gameObject);
            //StartCoroutine(Destroy());
        }
    }
    IEnumerator Destroy()
    {
        //goPoof.Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
           
    }
}
