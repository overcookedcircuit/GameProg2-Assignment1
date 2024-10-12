using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score = 0; // Keep score as a normal variable (not static)

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This keeps the GameManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
    }

    public void IncrementScore()
    {
        score += 50;
        Debug.Log(score);
        ScoreTextManager[] scoreTextManagers = FindObjectsOfType<ScoreTextManager>();
        foreach (var manager in scoreTextManagers)
        {
            manager.UpdateScoreDisplay();
        }
    }

    public void ResetScore()
    {
        score = 0; // Reset score if needed, but it won't happen on scene change
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
