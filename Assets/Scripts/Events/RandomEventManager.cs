using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public float eventStartTime = 20f;
    public float eventEndTime = 50f;
    public GameManager gameManager;
    public float minEventCooldown = 20f;
    public float minEventGeneratorCooldown = 1f;

    private bool eventsEnabled = true;
    private float lastEventTime = -Mathf.Infinity;
    private float lastEventCheckTime = -Mathf.Infinity;

    private float eventThreshold = 0.5f;

    public void Start()
    {
        lastEventTime = -Mathf.Infinity;
    }

    private void Update()
    {
        if (eventsEnabled && gameManager.movieProgress.elapsedTime >= eventStartTime && gameManager.movieProgress.elapsedTime <= eventEndTime)
        {
            if (gameManager.movieProgress.elapsedTime - lastEventTime >= minEventCooldown)
            {
                if(gameManager.movieProgress.elapsedTime - lastEventCheckTime >= minEventGeneratorCooldown)
                {
                    if (CheckIfEvent())
                    {
                        TriggerRandomEvent();
                    }
                }                
            }
        }
    }

    private void TriggerRandomEvent()
    {
        bool eventFound = false;

        do
        {
            float randomValue = Random.value;

            if (randomValue <= 0.5f)
            {
                if (!gameManager.powerManager.eventTriggered)
                {
                    gameManager.TriggerPowerShutDown();
                    eventFound = true;
                }
            }
            else if (randomValue > 0.5f)
            {
                if (!gameManager.acManager.eventTriggered)
                {
                    gameManager.TriggerACShutDown();
                    eventFound = true;
                }
            }
            /*else
            {
                if (!gameManager.foodAndDrinksManager.foodAndDrinksReady)
                {
                    gameManager.TriggerPopcornAndSoda();
                    eventFound = true;
                }
            }*/

            lastEventTime = gameManager.movieProgress.elapsedTime;
        }
        while (!eventFound);
    }

    private bool CheckIfEvent()
    {
        float randomValue = Random.value;

        if (randomValue <= eventThreshold)
        {
            lastEventCheckTime = gameManager.movieProgress.elapsedTime;
            eventThreshold = 0.5f;
            Debug.Log("Event!");
            return true;
        }
        else
        {
            lastEventCheckTime = gameManager.movieProgress.elapsedTime;
            eventThreshold += 0.1f;
            Debug.Log("No Event!");
            return false;
        }
    }
}
