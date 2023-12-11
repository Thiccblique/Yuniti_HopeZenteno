using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricController : MonoBehaviour
{
    public static IsometricController instance;

    public Rigidbody rb;
    
    public float speed;
    public float solidSpeed = 10;
    public float stop = 0f;
    public float rotSpeed = 360;
    public Vector3 input;
    public GameObject hitBox;
    private Animator anim;
    public Animator orionAnim;
    public GameObject particals;

    [SerializeField] public CameraZoom cameraZoom;

    void Start()
    {
        speed = solidSpeed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(cameraZoom.zoom <= cameraZoom.minZom)
        {
            speed = solidSpeed;
            PlayerInput();
            Look();
            Animation();
            AttackAnim();
        }
        else
        {
            speed = stop;
            anim.SetBool("Walk", false);
            particals.SetActive(false);
        }
       
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void PlayerInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (input == Vector3.zero) return;
        {
            var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        rb.MovePosition(transform.position + transform.forward * input.normalized.magnitude * speed * Time.deltaTime);
    }

    public void AttackAnim()
    {
        if (Input.GetKey(KeyCode.O) || Input.GetMouseButton(0) /*|| Input.GetKeyDown(KeyCode.Space)*/)
        {
            hitBox.SetActive(true);
            orionAnim.SetBool("OrionAttack", true);
        }
        else
        {
            orionAnim.SetBool("OrionAttack", false);
            hitBox.SetActive(false);
        }
    }

    public void Animation()
    {
       

        var hitKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow);
    
        if (hitKey == true)
        {
            // Activate the animation
            anim.SetBool("Walk", true);
            particals.SetActive(true);
        }
        else
        {
            // Activate the animation
            anim.SetBool("Walk", false);
            particals.SetActive(false);
        } 
      
        
    }

    public void PlayerStop()
    {
        speed = stop;
        anim.SetBool("Walk", false);
        particals.SetActive(false);
    }
    
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}