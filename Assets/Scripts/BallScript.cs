using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public BallLauncher ballLauncher;
    // Start is called before the first frame update
    void Start()
    {
        if (ballLauncher == null)
        {
            ballLauncher = GameObject.Find("Ball Launcher").GetComponent<BallLauncher>();
            if (ballLauncher == null)
            {
                Debug.LogError("Ball Launcher not found in the scene.");
            }
        }
    }

    void Update()
    {
        // If the ball falls below a certain point, destroy it and remove it from the ball launcher list.
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        if (ballLauncher != null)
        {
            ballLauncher.balls.Remove(gameObject);
        }
    }
}