using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject[] followers; // Array to hold follower GameObjects
    public float followSpeed = 10f; // Speed of followers
    public float stopDistance = 1.5f; // Distance to stop behind the target GameObject
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

    void Update()
    {
        // Iterate through each follower starting from the last one
        for (int i = followers.Length - 1; i > 0; i--)
        {
            // Calculate the direction from the current follower to the previous one
            Vector3 directionToPreviousFollower = followers[i - 1].transform.position - followers[i].transform.position;

            // Calculate the target position
            Vector3 targetPosition = followers[i - 1].transform.position - directionToPreviousFollower.normalized * stopDistance;

            // Move the current follower towards the target position
            followers[i].transform.position = Vector3.MoveTowards(followers[i].transform.position, targetPosition, Time.deltaTime * followSpeed);

            // Apply smooth up and down movement
            ApplySmoothUpDownMovement(followers[i].transform, i);
        }

        // Move the first follower to follow the player
        followers[0].transform.position = Vector3.MoveTowards(followers[0].transform.position, transform.position, Time.deltaTime * followSpeed);

        // Apply smooth up and down movement for the first follower
        ApplySmoothUpDownMovement(followers[0].transform, 0);
    }

    // Function to apply smooth up and down movement to a transform
    void ApplySmoothUpDownMovement(Transform targetTransform, int index)
    {
        // Calculate the floating movement using a sine function
        float floatOffset = Mathf.Sin(Time.time * floatSpeed + index) * floatHeight;

        // Apply the floating movement to the transform's position
        targetTransform.position = startPos[index] + new Vector3(0.0f, floatOffset, 0.0f);
    }

}
