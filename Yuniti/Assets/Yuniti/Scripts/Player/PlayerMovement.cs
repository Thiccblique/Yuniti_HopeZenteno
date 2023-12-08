using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotateSpeed = 180.0f;

    public Rigidbody rb;
    public ConstantForce gravity;

    void FixedUpdate()
    {
        PlayerInputs();
    }

    public void PlayerInputs()
    {
        // Get input for movement and rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 moveDirection = transform.forward * verticalInput;

        // Rotate the horse based on horizontal input
        transform.Rotate(Vector3.up * horizontalInput * rotateSpeed * Time.deltaTime);

        // Apply movement
        rb.MovePosition(transform.position + (moveDirection * moveSpeed * Time.deltaTime));

        //Adjusting gravity force
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gravity = gameObject.AddComponent<ConstantForce>();
        gravity.force = new Vector3(0.0f, -25, 0.0f);
    }

   
}
