using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header("Location of the ball launcher barrel.")]
    [SerializeField] private Transform barrelOut;

    [Header("Ball prefab to launch. May vary.")]
    [SerializeField] private GameObject ball;
    
    [Header("Time between ball launches in seconds.")]
    [SerializeField] private float timeBetweenLaunch;   
    
    [Header("The velocity of the launched ball in float.")]
    [SerializeField] private float minimumStartingVelocity;
    [SerializeField] private float maximumStartingVelocity;
    private float launchVelocity;

    [Header("The amount of balls in the given level.")]
    [SerializeField] private int totalBalls;

    [Header("The balls currently in the scene.")] // Accessible from the BallScript to manage removing instances.
    [SerializeField] public List<GameObject> balls = new List<GameObject>();

    private GameObject ballInstance;

    // Start is called before the first frame update

    void Start()
    {
        if (barrelOut == null)
        {
            Debug.LogError("barrelout not defined.");
            return;
        }
        if (ball == null)
        {
            Debug.LogError("ball is null");
            return;
        }
        if (timeBetweenLaunch == 0.0f)
        {
            Debug.LogError("timebetweenlaunch is not defined");
            return;
        }
        if (minimumStartingVelocity == 0.0f)
        {
            Debug.LogError("minimum velocity not defined");
            return;
        }
        if (maximumStartingVelocity == 0.0f)
        {
            Debug.LogError("Maximum Starting Velocity not defined.");
            return;
        }
        if (totalBalls == 0)
        {
            Debug.LogError("totalBalls is not defined.");
            return;
        }

        // For now, try launching some balls.
        StartLaunchingBalls();
    }

    void Update()
    {
        
    }

    // Balls to start launching once the game begins.
    public void StartLaunchingBalls()
    {
        StartCoroutine(LaunchBalls());
    }

    /*
    // Balls should stop launching as soon as totalBalls hits zero.
    public void StopLaunchingBalls()
    {
        StopCoroutine(LaunchBalls());
    }
    */

    IEnumerator LaunchBalls()
    {
        while (true)
        {
            // Pick a random velocity to launch the ball.
            launchVelocity = Random.Range(minimumStartingVelocity, maximumStartingVelocity);

            ballInstance = Instantiate(ball, barrelOut.position, barrelOut.rotation);
            ballInstance.GetComponent<Rigidbody>().velocity = barrelOut.up * launchVelocity;
            balls.Add(ballInstance);
            totalBalls -= 1;

            // Update the UI to reflect the number of balls left.
            GameManager.UpdateBallsLeftText(totalBalls);

            // Break if we run out of balls.
            if (totalBalls <= 0)
            {
                StartCoroutine(EndDay());
                break;
            }

            yield return new WaitForSeconds(timeBetweenLaunch);
        }
    }

    IEnumerator EndDay()
    {
        // Wait until all balls are gone before ending the day.
        while (balls.Count > 0)
        {
            yield return null;
        }
        GameManager.endDay();
    }
}
