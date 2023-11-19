using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public float rotationSpeed = 5f; // Adjust the speed of movement
    
    private Camera mainCamera;
  
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouseCursor();
    }

    private void FaceMouseCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0; // Ensure the object stays upright (for a top-down view)

            // Calculate rotation to look at the mouse position
            Quaternion rotation = Quaternion.LookRotation(lookDir);

            // Smoothly rotate towards the mouse cursor
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
            Debug.DrawLine(transform.position, lookDir, Color.white, Time.deltaTime);
        }
    
       
    }
      

    

}
