using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    void Start()
    {
        restartButton = GetComponent<Button>();

        restartButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance != null)
                GameManager.Instance.ReloadScene();
        });
    }
}
