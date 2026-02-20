using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private IntSelector intSelector;

    void Start()
    {
        restartButton = GetComponent<Button>();

        restartButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance != null)
            {
                intSelector.FinalizeBalls();
                GameManager.NextLevel();
            }
        });
    }
}
