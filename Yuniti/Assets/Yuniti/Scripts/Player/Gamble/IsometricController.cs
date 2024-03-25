using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.ProBuilder.MeshOperations;

public class IsometricController : MonoBehaviour
{
    public static IsometricController instance;
    private AudioManager sound;
    public Rigidbody rb;
    public float attackDamage = 1;
    public float swordDamage = 0.5f;
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
    public GameObject healthBar;
    public GameObject range;


    [Header("Scripts")]
    public GambleTower gambleTower;
    public PlayerHealth gambleHelath;
    

    [Header("Stamana")]
    public GameObject stamanaSlider;
    public float sprintSpeed = 15f;
    public float maxStamana = 1f;
    public float stamana;
    public Slider stamanaBar;

    [Header("Orion")]
    public Transform saddle;
    public GameObject freeOrion;
    public GameObject onOrion;
    public OrionController orionController;


  
    
       
    
    void Start()
    {
        stamana = maxStamana;
        sound = GetComponent<AudioManager>();
        speed = solidSpeed;
        anim = GetComponent<Animator>();
        SetMaxStamana(maxStamana);
        stamanaSlider.SetActive(false);
    }

    
    void Update()
    {
        Dismount();
        //if (cameraZoom.zoom <= cameraZoom.minZom && orionController.isMounted == true)
        //{
            speed = solidSpeed;
            PlayerInput();
            Look();
            Animation();
            AttackAnim();
            PlayerSprint();
            SetStamana(stamana);
            StamanaBack();
           
        //}
        /*else
        {
            //speed = stop;
            anim.SetBool("Walk", false);
            particals.SetActive(false);
            FindAnyObjectByType<AudioManager>().Stop("PlayerWalk");
        }*/
       

    }

    private void FixedUpdate()
    {
        Move();
    }

    
    public void Dismount()
    {
        if (orionController.isMounted == true)
        { 
            if(Input.GetKeyDown(KeyCode.Space))
            {
                FindAnyObjectByType<AudioManager>().Play("Jump");
                healthBar.SetActive(true);
                range.SetActive(true);
                gambleTower.enabled = true;
                saddle.DetachChildren();
                GameManager.instance.DisableAllMeshMounted();
                freeOrion.SetActive(true);
                orionController.isMounted = false;
                this.enabled = false;
                gambleHelath.enabled = true;
               

            }
           
        }
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
        Vector3 desiredMovement = transform.forward * input.normalized.magnitude * speed * Time.deltaTime;

        RaycastHit hit;
        if (rb.SweepTest(desiredMovement.normalized, out hit, desiredMovement.magnitude))
        {
            if (!hit.collider.isTrigger)
            {
                desiredMovement = Vector3.ProjectOnPlane(desiredMovement, hit.normal);
            }
        }
        rb.MovePosition(rb.position + desiredMovement);
    }

    private void PlayerSprint()
    {
        float realTime = Time.deltaTime;
        if(Input.GetKey(KeyCode.LeftShift) && stamana > .1 && speed > 1)
        {
            
            speed = sprintSpeed;
            stamana -= realTime;
            stamanaSlider.SetActive(true);
        }
        else
        {
            stamanaSlider.SetActive(false);
            realTime = 0;
            speed = solidSpeed;
        }
        
    }
    private void StamanaBack()
    {
        float realTime = Time.deltaTime;
        if (speed < sprintSpeed && stamana < maxStamana && !(Input.GetKeyDown(KeyCode.LeftShift)))
        {
            stamana += realTime;
        }
        else
        {
            realTime = 0;
        }
    }
    public void SetMaxStamana(float stamana)
    {
        stamanaBar.maxValue = stamana;
        stamanaBar.value = stamana;
    }
    public void SetStamana(float stamana)
    {
        stamanaBar.value = stamana;
    }

    public void AttackAnim()
    {
        if (Input.GetKey(KeyCode.O) || Input.GetMouseButtonDown(0) /*|| Input.GetKeyDown(KeyCode.Space)*/)
        {
            hitBox.SetActive(true);
            orionAnim.SetBool("OrionAttack", true);
            FindAnyObjectByType<AudioManager>().Play("HatThrow");
            //HitPartical();
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