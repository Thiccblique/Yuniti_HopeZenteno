using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaior : MonoBehaviour
{
    public float rotationSpeed = 150.0f;
    public Transform targetObject; 
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
        MoveToTarget();
    }

    private void Spin()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    
    private void MoveToTarget()
    {
        Vector3 direction = targetObject.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.coins++;
            Destroy(gameObject);
            FindAnyObjectByType<AudioManager>().Play("CoinPickup");
            GameManager.instance.score += 18;
        }
    }

}
