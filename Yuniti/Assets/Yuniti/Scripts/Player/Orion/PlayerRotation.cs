using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Camera cam;
    private MouseCoordinates mouseCoordinates;
    public float rotationSpeed = 5f;
    public LayerMask layer;
    Ray ray;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitDataArray = Physics.RaycastAll(ray, Mathf.Infinity, layer);

        foreach (var hitData in hitDataArray)
        {
            if (hitData.transform.CompareTag("Ground"))
            {
                mouseCoordinates = hitData.collider.GetComponent<MouseCoordinates>();

                // Calculate the direction from the character to the mouse position
                Vector3 direction = mouseCoordinates.worldPosition - transform.position;
                direction.y = 0f; // Keep the rotation only in the horizontal plane

                // Rotate the character towards the mouse position
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            }
        }
    }
}
