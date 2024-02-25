using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;


public class GambleEarth : MonoBehaviour
{
    public Animator animator;
    Vector2 movement;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject particals;
    private void Start()
    {
        animator.SetBool("OnEarth", true);
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector3 lookDirection = new Vector3(movement.x, movement.y).normalized;
        RunAnimation();
        LookDirection();
    }




    private void RunAnimation()
    {

        var hitKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow);
        if (hitKey == true)
        {
            // Activate the animation
            animator.SetBool("EarthWalk", true);
            particals.SetActive(true);
            UpdateAnimationsAndMove();


        }
        else
        {
            // Activate the animation
            animator.SetBool("EarthWalk", false);
            particals.SetActive(false);

        }
    }
    void UpdateAnimationsAndMove()
    {

        animator.SetFloat("Horizontal", lookDirection.x);
        animator.SetFloat("Vertical", lookDirection.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void LookDirection()
    {
        Vector2 move = new Vector2(movement.x, movement.y);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
    }
}
