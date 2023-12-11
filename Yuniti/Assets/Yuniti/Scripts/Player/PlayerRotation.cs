using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;

    // Update is called once per frame
    void Update()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float mouseY = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0, h, 0);

        if (mouseY > 0) 
        {
             transform.Rotate(0, mouseY, 0);
        }
    }
}
