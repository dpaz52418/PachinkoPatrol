using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeletionZone : MonoBehaviour
{
    [Header("The types of balls we can detect.")]
    [SerializeField] private static List<string> ballTags = new List<string>{"BasicBall"};

    [Header("The Game Manager.")]
    [SerializeField] public GameManager gameManager;

    [Header("The tile's tag.")]
    private string tileTag = "";

    /*

    [Header("Amount to count towards score when ball enters deletion zone.")]
    [SerializeField] private int amountToCount;
    */


    // Start is called before the first frame update
    void Start()
    {
        if (tileTag == "")
        {
            tileTag = gameObject.tag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        string resultingBall = "";

        if (ballTags.Contains(collision.gameObject.tag))
        {
            //Debug.Log("Collided with a ball!");
            resultingBall = collision.gameObject.tag;
            Destroy(collision.gameObject);
        }
 
        if (tileTag == "Score Tile")
        {
            switch (resultingBall) {
                case "BasicBall":
                    GameManager.UpdateScore(1);
                    break;
                default:
                    break;
            }
        }

    }
}
