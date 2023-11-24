using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public float rotationSpeed = 5f;
    public float playerRange = 3f;
    private int maxTargets = 1;
    private EnemyBehaviour enemyBehaviour;
    public LayerMask enemy;
    private Camera mainCamera;
    public Transform lookPoint;

    private List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
       //FaceMouseCursor();
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerRange);
    }

   /* void Detect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerRange, enemy); // Detects colliders in a layermask using OverlapSphere.
        foreach (var collider in hitColliders) // for each enemy collider detected in the OverlapSphere...
        {
            GameObject detectedEnemy = collider.gameObject; // a gameobject is created based on the enemy collider which allows the usage of gameobject methods.
            if (!targets.Contains(detectedEnemy) && targets.Count < maxTargets) // This adds enemies to the targets list depending if they weren't already added and the list has not reached max capacity.
            {
                targets.Add(detectedEnemy);
            }
        }

        foreach (var target in targets.ToArray()) // This shoots at each enemy inside the targets list.
        {
            if (target == null)
            {
                targets.Remove(target);
            }
            else
            {
                FaceMouseCursor(target);
                IsometricController.instance.AttackAnim();
            }
        }
    }*/

    private void FaceMouseCursor()//(GameObject enemy)
    {
        //lookPoint.transform.LookAt(enemy.transform);

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
