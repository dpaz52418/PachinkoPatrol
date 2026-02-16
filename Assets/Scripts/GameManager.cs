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

    [Header("The current text displaying the score.")]
    // Note that this might change between the two levels. Will not have anything on the Title Screen.
    [SerializeField] private TMPro.TextMeshProUGUI scoreText = null;

    public static GameManager Instance { get; private set; }

    [SerializeField] private SceneLoader sceneLoader;
    

    void Awake()
    {
	 if (Instance != null && Instance != this)
	 {
	     Destroy(gameObject);
         SceneManager.sceneLoaded += OnSceneLoaded;
	 }
	 else
	 {
            Instance = this;
            DontDestroyOnLoad(gameObject);
	 }


     if (sceneLoader == null)
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
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
        if (scene.name == "Title Screen")
        {
            return;
        }
        else
        {
            Instance.currentScore = 0;
            Instance.scoreText = GameObject.Find("Temporary Score Keeper").GetComponent<TextMeshProUGUI>();
        }
    }

}

