using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneLoader.Instance.mainMenu();
    }

    public void StartTutorial()
    {
        SceneLoader.Instance.loadTutorial();
    }

    public void NextLevel()
    {
        SceneLoader.Instance.LoadThisScene("Pachinko1");
    }
}
