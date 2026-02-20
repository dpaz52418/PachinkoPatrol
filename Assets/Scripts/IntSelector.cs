using UnityEngine;
using TMPro;

public class IntSelector : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    private int value = 1;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        UpdateDisplay();
    }

    public void Increase()
    {
        Debug.Log("made it here");
        if (value < gameManager.currentBalance)
        {
            value++;
            UpdateDisplay();
        }
    }

    public void Decrease()
    {
        if (value > 1)
        {
            value--;
            UpdateDisplay();
        }
    }

    public void FinalizeBalls()
    {
        gameManager.ballsThisLevel = value;
        gameManager.currentBalance -= value;
        GameManager.UpdateBalanceText(gameManager.currentBalance);
        Debug.Log("Purchased " + value + " balls. New balance: $" + gameManager.currentBalance);
    }

    void UpdateDisplay()
    {
        valueText.text = value.ToString();
    }
}