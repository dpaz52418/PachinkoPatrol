using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIAnimator : MonoBehaviour
{
    // Start is called before the first frame update

    // Script to manage the UI animations in response to onDayEnd in GameManager.

    public UnityEvent TriggerUIAnimations;

    void Start()
    {
        GameManager.Instance.onDayEnd.AddListener(AnimateDayEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AnimateDayEnd()
    {
        //Debug.Log("Animating the day end...");
        TriggerUIAnimations.Invoke();
        
    }
}
