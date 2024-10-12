using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of the obstacle movement
    public float distance = 10f;        // How far the obstacle moves from the center
    private float startX;               // Starting X position of the obstacle

    private void Start()
    {
        // Store the starting X position
        startX = transform.position.x;
    }

    private void Update()
    {
        // Calculate the new position based on a sine wave (side-to-side movement)
        float newX = startX + Mathf.Sin(Time.time * moveSpeed) * distance;

        // Update the position of the obstacle
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
