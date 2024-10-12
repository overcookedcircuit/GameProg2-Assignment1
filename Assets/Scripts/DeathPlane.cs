using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Call the method to reset the game
            ResetGame();
        }
    }

    private void ResetGame()
    {
        // Reset the score (you might need to call a method from your GameManager)
        GameManager.Instance.ResetScore(); // Ensure you have a method to reset the score

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
