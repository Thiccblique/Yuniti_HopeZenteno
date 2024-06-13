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
            if (hitData.transform.CompareTag("LookRotation"))
            {
                worldPosition = hitData.point; 
            
            }
        }
    }
}
