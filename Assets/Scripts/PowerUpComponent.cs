using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpComponent : MonoBehaviour
{
    private ParticleSystem explosionEffect;
    private MeshRenderer cubeMeshRenderer;
    public float respawnTime = 5f;
    private bool isCollected = false;

    void Start()
    {
        // Get the Particle System component attached to the object
        explosionEffect = GetComponentInChildren<ParticleSystem>();
        // Get the Mesh Renderer to hide the cube on collision
        cubeMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            // Get the CharacterMovement script and enable double jump
            CharacterMovement character = other.GetComponent<CharacterMovement>();
            if (character != null)
            {
                character.EnableDoubleJump();
            }

            // Play the explosion particle effect
            if (explosionEffect != null)
            {
                explosionEffect.Play();
            }

            // Hide the cube immediately
            if (cubeMeshRenderer != null)
            {
                cubeMeshRenderer.enabled = false; // Makes the cube invisible
            }

            // Start the respawn coroutine
            StartCoroutine(RespawnPowerUp());
        }
    }

    private IEnumerator RespawnPowerUp()
    {
        // Wait for the respawn time
        yield return new WaitForSeconds(respawnTime);

        // Reset state and make the cube visible again
        ResetState();

        // Optional: Position the new power-up at the original position
        // This will work if the power-up object is instantiated with the same prefab
        // The position should be adjusted as needed if you want it to respawn in a different place
        Instantiate(gameObject, transform.position, Quaternion.identity);
    }

    public void ResetState()
    {
        isCollected = false; // Reset the collected state to allow collection again

        // Make the cube visible again and stop the particle effect
        if (cubeMeshRenderer != null)
        {
            cubeMeshRenderer.enabled = true; // Makes the cube visible again
        }

        if (explosionEffect != null)
        {
            explosionEffect.Stop(); // Stops the particle effect
            explosionEffect.Clear(); // Clears any remaining particles
        }
    }
}
