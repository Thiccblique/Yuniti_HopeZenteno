using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMover : MonoBehaviour
{

    public float rotationSpeed = 5f; // Adjust this value to control the rotation speed
    public float tiltSpeed = 2f; // Adjust this value to control the tilt speed
    public float tiltAngleLimit = 30f; // Limit the maximum tilt angle

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the sphere
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        // Check for user input for rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate rotation amount based on input
        float rotationAmount = rotationSpeed * Time.fixedDeltaTime;

        // Apply torque for rotation
        rb.AddTorque(Vector3.back * horizontalInput * rotationAmount);
        rb.AddTorque(Vector3.right * verticalInput * rotationAmount);

        // Calculate tilt amount based on current velocity
        float tiltAmount = Mathf.Clamp(rb.velocity.magnitude * tiltSpeed, 0f, tiltAngleLimit);

        // Apply tilt to the object
        Quaternion targetRotation = Quaternion.Euler(tiltAmount, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * tiltSpeed);
    }
}
