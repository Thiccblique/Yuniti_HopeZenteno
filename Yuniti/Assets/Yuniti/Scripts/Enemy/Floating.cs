using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float floatHeight = 0.5f; 
    public float floatSpeed = 1.0f; 

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector3(0.0f, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0.0f);
    }

}
