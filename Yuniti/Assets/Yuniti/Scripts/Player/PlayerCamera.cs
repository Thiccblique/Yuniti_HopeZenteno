using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public OrionController orionController;
    public Transform target;
    public float smoothSpeed = 1f;
    public Vector3 offset;
    public Transform orion;
    public Transform gamble;
   
   /* private void Update()
    {
        if (!(orionController.isMounted = false))
        {
            target = gamble;
        }
        else
        {
            target = orion;
        }
    }*/
    void LateUpdate()
    {
        if (orionController.isMounted)
        {
            // Calculates position for camera and moves it
            Vector3 desiredPosition = gamble.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            // Calculates position for camera and moves it
            Vector3 desiredPosition = orion.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
      
    }
}
