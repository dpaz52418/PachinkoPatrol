using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    Vector2 movement;
    public float speed = 5f;
    [SerializeField] private Rigidbody rb;

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("Movement Input: " + movement);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        Vector3 direction = new Vector3(movement.x, movement.y, 0).normalized;
        if (direction.magnitude == 0)
        {
            if (rb.velocity.magnitude > 0)
            {
                //rb.velocity = rb.velocity * 0.99f * Time.deltaTime;
            }
            return;
        }
        rb.velocity = direction * speed;
        //transform.position += direction * speed * Time.deltaTime;
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Collided with Wall");
        }
    }
}
