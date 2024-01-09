using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDisable : MonoBehaviour
{
    public GameObject hatHitBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerstay(Collider other)
    {
        if (other.gameObject.CompareTag("Orion"))
        {
            hatHitBox.gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Orion"))
        {
            hatHitBox.gameObject.SetActive(true);
        }
    }

}
