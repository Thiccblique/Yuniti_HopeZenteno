using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public float moveSpeed = 5f; // Adjust the speed of movement
    
    private Camera mainCamera;
  
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FaceMouseCursor()
    {
        Vector3 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 90;

        transform.rotation = Quaternion.Euler(0f, angleDeg, 0f);
        Debug.DrawLine(transform.position, mousePos, Color.white, Time.deltaTime);

    }

}
