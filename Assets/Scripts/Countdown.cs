using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        Time.timeScale = 0f;   // Freeze everything

        yield return CountdownStep("3");
        yield return CountdownStep("2");
        yield return CountdownStep("1");
        yield return CountdownStep("GO!");

        countdownText.gameObject.SetActive(false);

        Time.timeScale = 1f;   // Unfreeze
    }

    IEnumerator CountdownStep(string message)
    {
        countdownText.text = message;
        yield return new WaitForSecondsRealtime(1f);  // IMPORTANT
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;   // Ensure time is unpaused if this object is disabled
    }
}