using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [Header("Scripts")]
    public EnemyBehaviour enBehavior;
   
    public Transform attackPoint; 
    public float attackRange = 5f; 
    public LayerMask attackLayer;

    private Animator anim;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouseCursor();
       
        if (Input.GetMouseButtonDown(0))
        { 
            Attack();
        }
        
       
    }

    private void FaceMouseCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = rotation;
        }
    }

    private void Attack()
    {
        
        //preforms attack at mouse cursor
        RaycastHit[] hits;
        hits = Physics.RaycastAll(attackPoint.position, transform.forward, attackRange, attackLayer);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Attacked: " + hit.transform.name);
            enBehavior.healthAmount -= 1;
          
        }
    }
}
