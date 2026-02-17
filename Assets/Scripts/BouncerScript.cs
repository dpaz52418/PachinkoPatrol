using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
    [Header("The types of balls we can detect.")]
    [SerializeField] private static List<string> ballTags = new List<string>{"BasicBall"};

    [Header("The force with which we will bounce the ball.")]
    [SerializeField] private float bounceForce = 10f;

    [Header("The transform of the bouncer.")]
    [SerializeField] private Transform bouncerTransform;

    [Header("Time for bouncer to extend its scale and back down.")]
    [SerializeField] private float bounceDuration = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log("Made it here");
            Debug.DrawRay(
                contact.point,
                contact.normal * 3f,
                Color.red,
                10f
            );
        }


        if (ballTags.Contains(collision.gameObject.tag))
        {
            Debug.Log("Collided with a ball.");
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounceDirection = Vector3.Reflect(collision.relativeVelocity.normalized, collision.contacts[0].normal);
            ballRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }


    }

    /*

    void ExtendandRetract()
    {
        
    }
    */
}
