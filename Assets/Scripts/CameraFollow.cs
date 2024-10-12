using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float distance = 5.0f;  // Distance from the player
    public float height = 2.0f;    // Height above the player
    public float rotationSpeed = 5.0f;  // Speed of camera rotation

    private float currentYRotation = 0.0f;

    void Update()
    {
        // Get mouse X movement to rotate around the player
        float mouseX = Input.GetAxis("Mouse X");
        currentYRotation += mouseX * rotationSpeed;

        // Calculate the new position based on rotation around the player
        Vector3 direction = new Vector3(0, height, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentYRotation, 0);
        Vector3 newPosition = player.position + rotation * direction;

        // Move the camera to the new position and look at the player
        transform.position = newPosition;
        transform.LookAt(player);
    }
}
