using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTextAnimation : MonoBehaviour
{

    public float floatSpeed = 5f;
    public float destroyDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        StartCoroutine(FloatUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FloatUp()
    {
        while (transform.position.y < 2f) 
        {
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
