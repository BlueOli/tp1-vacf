using UnityEngine;
using UnityEngine.UI;

public class MovieProgress : MonoBehaviour
{
    public Image progressBar; // Reference to the UI image for movie progress bar
    public float movieDuration = 60f; // Duration of the movie in seconds
    public GameManager gameManager; // Reference to the GameManager script

    private float elapsedTime = 0f; // Elapsed time since the start of the movie

    private void Start()
    {
        // Set initial fill amount of the progress bar to 0
        progressBar.fillAmount = 0f;
    }

    private void Update()
    {
        if (!gameManager.gameEnded)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate fill amount for the progress bar
            float fillAmount = elapsedTime / movieDuration;
            fillAmount = Mathf.Clamp01(fillAmount); // Clamp fill amount between 0 and 1

            // Update progress bar fill amount
            progressBar.fillAmount = fillAmount;

            // Check for movie completion
            if (elapsedTime >= movieDuration)
            {
                // Movie has ended, trigger win condition
                gameManager.Victory();
            }
        }        
    }
}
