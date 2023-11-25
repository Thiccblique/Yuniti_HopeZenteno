using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 200f;

    public bool left = true;
    public bool right = true;

    public bool topMovement = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(topMovement)
        {
            UpDown();
        }
       else
        {
            LeftRight();
        }

      

    }

    private void UpDown()
    {
        if (left)
        {
            // Rotate the object around the Y-axis
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.down, rotationSpeed * Time.deltaTime);
        }

    }
    private void LeftRight()
    {
        if (right)
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }
    }
    
}
