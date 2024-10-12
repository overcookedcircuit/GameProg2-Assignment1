using UnityEngine;
using UnityEngine.UI;

using TMPro; // Add this line at the top

public class ScoreTextManager : MonoBehaviour
{
    public TMP_Text scoreText; // Change to TMP_Text

    private void Start()
    {
        UpdateScoreDisplay(); // Update the display at the start
    }

    public void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.score.ToString();
        }
    }
}
