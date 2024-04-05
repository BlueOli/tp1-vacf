using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance of GameManager
    public FriendSpawner friendSpawner; // Reference to the FriendSpawner script
    public UnknownPersonSpawner unknownPersonSpawner; // Reference to the UnknownPersonSpawner script
    public GameObject playerPrefab; // Reference to the player prefab
    public Transform spawnPoint; // Spawn point for the player

    public float kickOutThreshold = 0; // Threshold at which the group gets kicked out

    public Text endGameText; // Reference to the UI text for end game message
    public KeyCode restartKey = KeyCode.R; // Key to restart the game
    public KeyCode continueKey = KeyCode.Space; // Key to continue the game
    public float restartDelay = 2f; // Delay before allowing restart after end game

    public bool gameEnded = false; // Flag to track if the game has ended

    public DifficultyManagerSO dificultyManagerSO;

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

        SpawnPlayer();
    }

    private void Update()
    {
        if (gameEnded)
        {
            AllowRestart();
            AllowContinue();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ContinueGame()
    {
        dificultyManagerSO.maxFriendNumber += 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
