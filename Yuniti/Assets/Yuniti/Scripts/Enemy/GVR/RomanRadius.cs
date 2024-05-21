using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomanRadius : MonoBehaviour
{
    public GameObject attackfight;
    private RomanBehaviour romanBehaviour;
    private RomanBehaviour greekBehaviour;
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
        if(other.CompareTag("GreekRadius"))
        {
            romanBehaviour = this.GetComponentInParent<RomanBehaviour>();
            greekBehaviour = other.GetComponentInParent<RomanBehaviour>();
            Instantiate(attackfight, other.transform);
            Instantiate(attackfight, this.transform);

            romanBehaviour.healthAmount *= 0.75f;
            greekBehaviour.healthAmount *= 0.75f;

        }
    }
}
