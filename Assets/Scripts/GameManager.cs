using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LoadGridLayout gridSO;

    public static GameManager Instance; // Singleton instance of GameManager
    public FriendSpawner friendSpawner; // Reference to the FriendSpawner script
    public UnknownPersonSpawner unknownPersonSpawner; // Reference to the UnknownPersonSpawner script
    public GameObject playerPrefab; // Reference to the player prefab
    public Transform spawnPoint; // Spawn point for the player
    public MovieProgress movieProgress;
    public Room gameRoom;

    public float kickOutThreshold = 0; // Threshold at which the group gets kicked out

    public Text endGameText; // Reference to the UI text for end game message
    public KeyCode restartKey = KeyCode.R; // Key to restart the game
    public KeyCode continueKey = KeyCode.Space; // Key to continue the game
    public float restartDelay = 2f; // Delay before allowing restart after end game

    public bool gameEnded = false; // Flag to track if the game has ended

    public DifficultyManagerSO dificultyManagerSO;

    public PowerManager powerManager;
    public ACManager acManager;
    public FoodAndDrinksManager foodAndDrinksManager;

    public float minTemperature = 10f;
    public float maxTemperature = 15f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log(gridSO.grid[0, 0]);

        if (gridSO.grid[0,0] != 0)
        {
            dificultyManagerSO.maxFriendNumber = gridSO.maxFriends;

            for (int i = 0; i < gridSO.grid.GetLength(0); i++)
            {
                for (int j = 0; j < gridSO.grid.GetLength(1); j++)
                {
                    switch (gridSO.grid[i, j])
                    {
                        case 1:
                            friendSpawner.SpawnFriendByPos(gridSO.grid.GetLength(0) - 1 - i, j);
                            break;
                        case -1:
                            unknownPersonSpawner.SpawnUnkownPersonByPos(gridSO.grid.GetLength(0) - 1 - i, j);
                            break;
                    }
                }
            }
        }
        else
        {
            friendSpawner.maxFriends = dificultyManagerSO.maxFriendNumber;

            // Call the SpawnFriends method of the FriendSpawner script
            if (friendSpawner != null)
            {
                friendSpawner.SpawnFriends();
            }

            // Call the SpawnUnknownPersons method of the UnknownPersonSpawner script
            if (unknownPersonSpawner != null)
            {
                unknownPersonSpawner.SpawnUnknownPersons();
            }
        }        

        SpawnPlayer();

        movieProgress.movieIsPaused = false;
    }

    private void Update()
    {
        if (gameEnded)
        {
            AllowRestart();
            AllowContinue();
        }
        else
        {
            if (movieProgress.movieIsFinished)
            {
                Victory();
            }

            if (powerManager.powerOut)
            {
                if (!movieProgress.movieIsPaused)
                {
                    movieProgress.movieIsPaused = true;
                }
            }
            else
            {
                if (movieProgress.movieIsPaused)
                {
                    movieProgress.movieIsPaused = false;
                }
            }

            if (acManager.ACOut)
            {
                gameRoom.temperature = maxTemperature;
            }
            else
            {
                gameRoom.temperature = minTemperature;
            }
        }       
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    // Method to kick out the group
    public void KickOut()
    {
        // Implement your logic here for what happens when the group gets kicked out
        Debug.Log("The group has been kicked out from the cinema!");
        // You can restart the level or display a game over screen, etc.

        if (!gameEnded)
        {
            gameEnded = true;
            movieProgress.movieIsPaused = true;
            endGameText.text = "Kicked out! Press " + restartKey.ToString() + " to Restart";
            endGameText.gameObject.SetActive(true);
        }
    }

    public void Victory()
    {
        Debug.Log("The group has seen the whole movie!");
        // You can restart the level or display a game over screen, etc.

        if (!gameEnded)
        {
            gameEnded = true;
            endGameText.text = "Good Job! Press " + continueKey.ToString() + " to Continue";
            endGameText.gameObject.SetActive(true);
        }
    }

    // Method to check if the group should be kicked out based on the patience of unknown people
    public void CheckPatienceThreshold(float patience)
    {
        if (patience <= kickOutThreshold)
        {
            KickOut();
        }
    }

    // Allow the player to restart the game
    private void AllowRestart()
    {
        // Listen for restart input
        if (Input.GetKeyDown(restartKey))
        {
            RestartGame();
        }
    }

    private void AllowContinue()
    {
        // Listen for restart input
        if (Input.GetKeyDown(continueKey))
        {
            ContinueGame();
        }
    }

    // Restart the game by reloading the current scene
    private void RestartGame()
    {
        dificultyManagerSO.maxFriendNumber = 9;
        gridSO.maxFriends = dificultyManagerSO.maxFriendNumber;
        if (gridSO.grid != null)
        {
            for (int i = 0; i < gridSO.grid.GetLength(0); i++)
            {
                for (int j = 0; j < gridSO.grid.GetLength(1); j++)
                {
                    gridSO.grid[i, j] = 0;
                }
            }
        }
        SceneManager.LoadScene(2);
    }

    private void ContinueGame()
    {
        dificultyManagerSO.maxFriendNumber += 3;
        gridSO.maxFriends = dificultyManagerSO.maxFriendNumber;
        if (gridSO.grid != null)
        {
            for (int i = 0; i < gridSO.grid.GetLength(0); i++)
            {
                for (int j = 0; j < gridSO.grid.GetLength(1); j++)
                {
                    gridSO.grid[i, j] = 0;
                }
            }
        }
        SceneManager.LoadScene(2);
    }

    #region Random Events

    public void TriggerPowerShutDown()
    {
        powerManager.EventTrigger();
    }

    public void TriggerACShutDown()
    {
        acManager.EventTrigger();
    }

    public void TriggerPopcornAndSoda()
    {
        foodAndDrinksManager.EventTrigger();
    }

    #endregion
}

