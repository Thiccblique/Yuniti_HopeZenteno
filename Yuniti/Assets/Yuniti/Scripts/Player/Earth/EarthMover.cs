using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMover : MonoBehaviour
{
    [Header("CircleFade")]
    public GameObject circleFade;

    [Header("Classic Filter")]
    public GameObject darkPost;

    public float moveSpeed = 5f; // Adjust this value to control movement speed
    public float rotationSpeed = 50f; // Adjust this value to control rotation speed
    private void Start()
    {
        CirclePlay();
        Invoke("CircleDeactive", 2.5f);
    }
    void Update()
    {
        // Get input from horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the sphere on the Y-axis based on horizontal input
        transform.Rotate(Vector3.down, -horizontalInput * rotationSpeed * Time.deltaTime, Space.World);

        // Calculate rotation angle on the X-axis based on vertical input
        float rotationAngleX = -verticalInput * rotationSpeed * Time.deltaTime;

        // Create a rotation quaternion around the X-axis
        Quaternion rotationX = Quaternion.AngleAxis(rotationAngleX, Vector3.right);

        // Apply rotation only on the X-axis
        transform.rotation = rotationX * transform.rotation;
    }

    private void CirclePlay()
    {
        Animator animator = circleFade.GetComponent<Animator>();
        animator.Play("Fadeout");

    }
    private void CircleDeactive()
    {
        circleFade.SetActive(false);
    }

    public void SelectAudio()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
    }

    public void DarkProcess()
    {
        if (darkPost.activeInHierarchy == false)
        {
            darkPost.SetActive(true);
        }
        else
        {
            darkPost.SetActive(false);
        }
    }
}
