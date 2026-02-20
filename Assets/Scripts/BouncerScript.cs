using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
    [Header("The types of balls we can detect.")]
    [SerializeField] private static List<string> ballTags = new List<string>{"BasicBall", "Play"};

    [Header("The force with which we will bounce the ball.")]
    [SerializeField] private float bounceForce = 10f;

    [Header("The transform of the bouncer.")]
    [SerializeField] private Transform bouncerTransform;

    [Header("Time for bouncer to extend its scale and back down.")]
    private float bounceDuration = 0.15f;

    //private bool isPopping = false;
    private Vector3 originalScale;
    private Coroutine bounceCoroutine;

    [SerializeField] private AudioSource bounceSound;



    // Start is called before the first frame update
    void Start()
    {
        if (bouncerTransform == null)
        {
            bouncerTransform = this.transform;
        }
        originalScale = bouncerTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            //Debug.Log("Made it here");
            Debug.DrawRay(
                contact.point,
                contact.normal * 3f,
                Color.red,
                10f
            );
        }


        if (ballTags.Contains(collision.gameObject.tag))
        {
            //Debug.Log("Collided with a ball.");
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounceDirection = Vector3.Reflect(collision.relativeVelocity.normalized, collision.contacts[0].normal);
            ballRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

            if (bounceSound != null)
            {
                bounceSound.Play();
            }
            
            if (bounceCoroutine != null)
            {
                StopCoroutine(bounceCoroutine);
            }
            bounceCoroutine = StartCoroutine(ExtendandRetract());
            
            
        }


    }

    
    
    IEnumerator ExtendandRetract()
    {
        Collider col = GetComponent<Collider>();
        col.enabled = false;

        float timer = 0f;
        float halfDuration = bounceDuration / 2f;

        Vector3 targetScale = new Vector3(2f,0.39f,2f);
        //Debug.Log("Target Scale: " + targetScale);

        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            bouncerTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            timer += Time.deltaTime;
            //Debug.Log("Current Scale: " + bouncerTransform.localScale);
            yield return null;
        }

        timer = 0f;

        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            bouncerTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            timer += Time.deltaTime;
            yield return null;
        }
        
        bouncerTransform.localScale = originalScale; // Ensure it ends at the original scale
        col.enabled = true;

    }
    
}