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

    [Header("Number of balls for each level.")]
    [SerializeField] private int ballsThisLevel;

    [Header("Current level the player is on. Settings change accordingly.")]
    [SerializeField] private int currentLevel = 0;

    [Header("Total score right now.")]
    [SerializeField] private int currentScore = 0;

    [Header("Total balance in money.")] // Temporarily in dollars for simplicity.
    [SerializeField] public int currentBalance = 0;

    [Header("The current text displaying the score.")]
    // Note that this might change between the two levels. Will not have anything on the Title Screen.
    [SerializeField] private TMPro.TextMeshProUGUI scoreText = null;
    [SerializeField] private TMPro.TextMeshProUGUI dayText = null;
    [SerializeField] private TMPro.TextMeshProUGUI balanceText = null;
    [SerializeField] private TMPro.TextMeshProUGUI ballsLeftText = null;
    public static GameManager Instance { get; private set; }

    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private BallLauncher ballLauncher = null;

    public UnityEvent onDayEnd;

    private bool goldenArchBought = false;
    

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
        Instance.scoreText.text = "Score: " + Instance.currentScore;
        Debug.Log("score updated: " + Instance.currentScore);
    }

    // Primarily for removing score, potentially for purchasing stuff.
    public static void RemoveScore(int amount)
    {
        Instance.currentScore -= amount;
        Instance.scoreText.text = "Current Score: " + Instance.currentScore;
        Debug.Log("score updated: " + Instance.currentScore);
    }

    // Cashes out the current score by multiplying it by 2 and adding to balance
    public static void CashOut()
    {
        Instance.currentBalance += Instance.currentScore * 5;
        Instance.currentScore = 0;
        Instance.scoreText.text = "Score: " + Instance.currentScore;
        UpdateBalanceText(Instance.currentBalance);
        Debug.Log("Cashed out... Balance: $" + Instance.currentBalance);
    }

    // Purchases the Golden Arch upgrade if balance is sufficient
    public static void PurchaseGoldenArch(int currentBalance)
    {
        if (currentBalance >= 150)
        {
            Instance.currentBalance -= 150;
            UpdateBalanceText(Instance.currentBalance);
            Instance.currentLevel++;
            UpdateDayText(Instance.currentLevel);
            Instance.goldenArchBought = true;
            Debug.Log("Golden Arch purchased! Level upgraded to: " + Instance.currentLevel);
        }
        else
        {
            Debug.Log("Insufficient balance! Need $150 but have $" + currentBalance);
        }
    }

    public static void NextLevel()
    {
        if (!Instance.goldenArchBought)
        {
            Debug.Log("Golden Arch not purchased yet! Cannot proceed to next level.");
            Instance.sceneLoader.LoadThisScene("Pachinko1");
        } else {
            Instance.sceneLoader.LoadThisScene("Pachinko2");
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

            if (ballLauncher == null)
            {
                ballLauncher = FindObjectOfType<BallLauncher>();
            }

            if (ballLauncher != null)
            {
                ballLauncher.SetTotalBalls(ballsThisLevel);
            }
        }
    }

    public static void endDay()
    {
        Instance.onDayEnd.Invoke();
        CashOut();
        Debug.Log("Day ended!");
    }

}

