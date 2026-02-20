using UnityEngine;
using TMPro;

public class IntSelector : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    private int value = 0;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        UpdateDisplay();
    }

    public void Increase()
    {
        if (value < gameManager.currentBalance)
        {
            value++;
            UpdateDisplay();
        }
    }

    public void Decrease()
    {
        if (value > 0)
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