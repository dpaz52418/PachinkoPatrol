using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionZone : MonoBehaviour
{
    [Header("The types of balls we can detect.")]
    [SerializeField] private List<string> ballTags;
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
