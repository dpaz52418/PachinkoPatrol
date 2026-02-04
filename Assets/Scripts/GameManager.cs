using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Levels in the game, ready for loading.")]
    [SerializeField] private string[] levels;

    [Header("Current level the player is on. Settings change accordingly.")]
    [SerializeField] private int currentLevel = 1;
  
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

}
