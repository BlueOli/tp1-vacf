using UnityEngine;
using UnityEngine.UI;

public class MovieProgress : MonoBehaviour
{
    public Image progressBar; 
    public float movieDuration = 60f; 
    public bool movieIsFinished = false;
    public bool movieIsPaused = true;

    public float elapsedTime = 0f;

    private void Start()
    {
        // Set initial fill amount of the progress bar to 0
        progressBar.fillAmount = 0f;
    }

    private void Update()
    {
        if (!movieIsPaused || movieIsFinished)
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
                if (!movieIsFinished)
                {
                    movieIsFinished = true;
                }                
            }
        }        
    }
}
