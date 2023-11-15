using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 1f;
    public Vector3 offset;

    // LateUpdate is used to so that the camera updates after the target's position has been updated
    void LateUpdate()
    {
        // Calculates position for camera and moves it
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
