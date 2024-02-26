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
        FindAnyObjectByType<AudioManager>().Play("BackGround");
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
        var hitKeySound = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow);
        var hitKeySoundUp = Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow);

        if (hitKeySound == true)
        {
            FindAnyObjectByType<AudioManager>().Play("PlayerWalk");
        }
        else if (hitKeySoundUp == true)
        {
            FindAnyObjectByType<AudioManager>().Stop("PlayerWalk");
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
