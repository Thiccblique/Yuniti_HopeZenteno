using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCoordinates : MonoBehaviour
{
    public Vector3 worldPosition;
    public LayerMask layer;
    Ray ray;
    
    void Start()
    {
    
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitDataArray = Physics.RaycastAll(ray, Mathf.Infinity, layer);
        
        foreach (var hitData in hitDataArray)
        {
            if (hitData.transform.CompareTag("Ground"))
            {
                //worldPosition = new Vector3(hitData.point.x, hitData.point.y + offset, hitData.point.z);

                worldPosition = hitData.point; 
            
            }
        }
    }
}
