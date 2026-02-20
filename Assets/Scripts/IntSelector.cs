using UnityEngine;
using TMPro;

public class IntSelector : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    private int value = 0;

    void Start()
    {
        UpdateDisplay();
    }

    public void Increase()
    {
        value++;
        UpdateDisplay();
    }

    public void Decrease()
    {
        if (value > 0)
        {
            value--;
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        valueText.text = value.ToString();
    }
}