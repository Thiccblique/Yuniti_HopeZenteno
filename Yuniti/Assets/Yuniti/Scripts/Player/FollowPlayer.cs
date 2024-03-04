using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject[] followers; // Array to hold follower GameObjects
    public float followSpeed = 5f; // Speed of followers
    public float stopDistance = 2f; // Distance to stop behind the target GameObject
    public float floatHeight = 0.1f; // Height of the floating movement
    public float floatSpeed = 1f; // Speed of the floating movement

    private Vector3[] startPos; // Array to store initial positions of followers

    void Start()
    {
        // Store initial positions of followers
        startPos = new Vector3[followers.Length];
        for (int i = 0; i < followers.Length; i++)
        {
            startPos[i] = followers[i].transform.position;
        }
    }

    void FixedUpdate()
    {
        // Move the first follower (player) with physics-based movement
        followers[0].GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(followers[0].transform.position, transform.position, Time.fixedDeltaTime * followSpeed));

        // Iterate through each follower starting from the second one
        for (int i = 1; i < followers.Length; i++)
        {
            // Calculate the direction from the current follower to the previous one
            Vector3 directionToPreviousFollower = followers[i - 1].transform.position - followers[i].transform.position;

            // Calculate the target position
            Vector3 targetPosition = followers[i - 1].transform.position - directionToPreviousFollower.normalized * stopDistance;

            // Move the current follower towards the target position directly
            followers[i].transform.position = Vector3.MoveTowards(followers[i].transform.position, targetPosition, Time.fixedDeltaTime * followSpeed);

            // Apply smooth up and down movement for each follower
            ApplySmoothUpDownMovement(followers[i].transform, i);
        }
    }

    // Function to apply smooth up and down movement to a transform
    void ApplySmoothUpDownMovement(Transform targetTransform, int index)
    {
        // Calculate the floating movement using a sine function
        float floatOffset = Mathf.Sin(Time.time * floatSpeed + index) * floatHeight;

        // Apply the floating movement to the transform's position
        Vector3 newPosition = startPos[index] + new Vector3(0.0f, floatOffset, 0.0f);

        // Preserve the original x and z positions for forward movement
        newPosition.x = targetTransform.position.x;
        newPosition.z = targetTransform.position.z;

        // Apply the new position
        targetTransform.position = newPosition;
    }
}
