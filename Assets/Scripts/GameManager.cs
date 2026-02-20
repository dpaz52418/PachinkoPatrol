using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Levels in the game, ready for loading.")]
    [SerializeField] private string[] levels;

    [Header("Current level the player is on. Settings change accordingly.")]
    [SerializeField] private int currentLevel = 0;

    [Header("Total score right now.")]
    [SerializeField] private int currentScore = 0;

    [Header("Total balance in money.")] // Temporarily in dollars for simplicity.
    [SerializeField] private int currentBalance = 0;

    [Header("The current text displaying the score.")]
    // Note that this might change between the two levels. Will not have anything on the Title Screen.
    [SerializeField] private TMPro.TextMeshProUGUI scoreText = null;
    [SerializeField] private TMPro.TextMeshProUGUI dayText = null;
    [SerializeField] private TMPro.TextMeshProUGUI balanceText = null;
    [SerializeField] private TMPro.TextMeshProUGUI ballsLeftText = null;
    public static GameManager Instance { get; private set; }

    [SerializeField] private SceneLoader sceneLoader;

    public UnityEvent onDayEnd;
    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;


     if (sceneLoader == null)
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
        }
    }

    public static void UpdateDayText(int day)
    {
        if (Instance.dayText != null)
        {
            Instance.dayText.text = "Day: " + day;
        }
    }

    public static void UpdateBalanceText(int balance)
    {
        if (Instance.balanceText != null)
        {
            Instance.balanceText.text = "Balance: $" + balance;
        }
    }

    public static void UpdateBallsLeftText(int ballsLeft)
    {
        if (Instance.ballsLeftText != null)
        {
            Instance.ballsLeftText.text = "Balls Left: " + ballsLeft;
        }
    }

    // Used for adding score, primarily from the board.
    public static void AddScore(int amount)
    {
        Instance.currentScore += amount;
        Instance.scoreText.text = "Current Score: " + Instance.currentScore;
        Debug.Log("score updated: " + Instance.currentScore);
    }

    // Primarily for removing score, potentially for purchasing stuff.
    public static void RemoveScore(int amount)
    {
        Instance.currentScore -= amount;
        Instance.scoreText.text = "Current Score: " + Instance.currentScore;
        Debug.Log("score updated: " + Instance.currentScore);
    }

    public static void NextLevel()
    {
        if (Instance.currentLevel < Instance.levels.Length - 1)
        {
            Instance.currentLevel++;
            Debug.Log("we're here");
            Instance.sceneLoader.LoadThisScene(Instance.currentLevel);
        }
        else
        {
            Debug.Log("No more levels to load!");
        }
    }

    // Temporary for reloading the scene for demo.
    public void ReloadScene()
    {
        sceneLoader.ReloadScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        if (scene.name == "Title Screen" || scene.name == "Tutorial")
        {
            return;
        }
        else
        {
            Instance.currentScore = 0;
            Instance.scoreText = GameObject.Find("Temporary Score Keeper").GetComponent<TextMeshProUGUI>();
            Instance.dayText = GameObject.Find("Day Indicator Text").GetComponent<TextMeshProUGUI>();
            UpdateDayText(Instance.currentLevel);

            Instance.balanceText = GameObject.Find("Current Balance").GetComponent<TextMeshProUGUI>();
            UpdateBalanceText(Instance.currentBalance);

            Instance.ballsLeftText = GameObject.Find("Balls Left").GetComponent<TextMeshProUGUI>();
        }
    }

    public static void endDay()
    {
        Instance.onDayEnd.Invoke();
        
        Debug.Log("Day ended!");
    }

}

