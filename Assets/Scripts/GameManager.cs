using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Levels in the game, ready for loading.")]
    [SerializeField] private string[] levels;

    [Header("Current level the player is on. Settings change accordingly.")]
    [SerializeField] private int currentLevel = 1;

    [Header("Total score right now.")]
    [SerializeField] private int currentScore = 0;

    [Header("The current text displaying the score.")]
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    [SerializeField] public UnityEvent onScoreChanged;

  
    public static GameManager Instance { get; private set; }

    void Awake()
    {
	 if (Instance != null && Instance != this)
	 {
	     Destroy(gameObject);
	 }
	 else
	 {
            Instance = this;
            DontDestroyOnLoad(gameObject);
	 }
    }

    public static void UpdateScore(int amount)
    {
        Instance.currentScore += amount;
        Instance.scoreText.text = "Current Score: " + Instance.currentScore;
    }

}
