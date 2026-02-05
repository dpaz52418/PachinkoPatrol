using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform pivotPoint;  // Assign shoulder/arm base position
    
    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    
    void Update()
    {
        // Create a ray from camera through mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        // Create a plane at the pivot's Z position
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, pivotPoint.position.z));
        
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPos = ray.GetPoint(distance);
            
            // Calculate direction from pivot to mouse (ignoring Z)
            Vector3 direction = mouseWorldPos - pivotPoint.position;
            direction.z = 0;  // Ensure no Z component
            
            // Calculate angle around Z-axis
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            // Apply rotation only on Z-axis
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
}