using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeletionZone : MonoBehaviour
{
    [Header("The types of balls we can detect.")]
    [SerializeField] private static List<string> ballTags;

    [Header("The Game Manager.")]
    [SerializeField] public GameManager gameManager;

    /*

    [Header("Amount to count towards score when ball enters deletion zone.")]
    [SerializeField] private int amountToCount;
    */


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
        if (ballTags.Contains(collision.gameObject.tag))
        {
            Debug.Log("Collided with a ball!");
            Destroy(collision.gameObject);
        }

        
    }
}
