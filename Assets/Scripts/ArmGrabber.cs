using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class ArmGrabber : MonoBehaviour
{
    [Header("Grab Settings")]
    [SerializeField] private Transform handPosition;  // Empty GameObject at hand location
    public List<string> grabbableTags = new List<string> { "BasicBall" };
    
    [Header("Throw Settings!")]
    public float throwForce = 5f;

    private Rigidbody armRb;
    private List<GameObject> ballsInRange = new List<GameObject>();
    private GameObject heldBall = null;
    private Rigidbody heldBallRb = null;

    void Start()
    {
        armRb = GetComponent<Rigidbody>();
    }
    
    void OnGrab(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("grab!");
            if (heldBall == null)
            {
                TryGrabBall();
            }
            else
            {
                DropBall();
            }
        }
    }
    
    void TryGrabBall()
    {
        // Find closest ball in range
        if (ballsInRange.Count > 0)
        {
            heldBall = ballsInRange[0];
            heldBallRb = heldBall.GetComponent<Rigidbody>();
            
            if (heldBallRb != null)
            {
                heldBallRb.isKinematic = true; 
            }
            
            Debug.Log("Grabbed: " + heldBall.name);
        }
    }
    
    void DropBall()
    {
        if (heldBall != null)
        {
            if (heldBallRb != null)
            {
                heldBallRb.isKinematic = false;

                Vector3 angularVel = armRb.angularVelocity;
                
                Vector3 handOffset = handPosition.position - transform.position;

                heldBallRb.velocity = handOffset * throwForce;
                
                Debug.Log("Threw with velocity: " + heldBallRb.velocity.magnitude);
            }
            
            Debug.Log("Dropped: " + heldBall.name);
            heldBall = null;
            heldBallRb = null;
        }
    }
    
    void Update()
    {
        // Keep ball at hand position while held
        if (heldBall != null && handPosition != null)
        {
            heldBall.transform.position = handPosition.position;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (grabbableTags.Contains(other.tag) && !ballsInRange.Contains(other.gameObject))
        {
            ballsInRange.Add(other.gameObject);
            //Debug.Log("Ball in range: " + other.name);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (ballsInRange.Contains(other.gameObject))
        {
            ballsInRange.Remove(other.gameObject);
            //Debug.Log("Ball left range: " + other.name);
        }
    }
}