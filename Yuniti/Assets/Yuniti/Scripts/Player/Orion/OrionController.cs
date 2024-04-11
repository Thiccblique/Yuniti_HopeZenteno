using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.ParticleSystem;

public class OrionController : MonoBehaviour
{
    public static OrionController instance;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 input;

    public bool isMounted = true;
    public bool inTutorial = false;
    public Transform saddle;
    public ParticleSystem hit;
    [Header("Aniamtion")]
    public Animator orionAnim;
    public GameObject onOrion;
    public Animator sword;

    [Header("Scripts")]
    public IsometricController gambleWalk;
    public GambleTower gambleTower;
    public PlayerHealth gamblehealth;
    public GameObject healthBar;
    public GameObject range;

    [Header("UI")]
    public GameObject space;

    [Header("Particls")]
    public GameObject runParticals;

    private void Awake()
    {
        gameObject.transform.SetParent(saddle);
        onOrion.SetActive(true);
        if(isMounted)
        {
            gameObject.SetActive(false);
        }
       
        //isMounted = true;
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (inTutorial == false)
        {
            GatherInput();
            Look();
            WalkAnim();
            AttackAnim();
        }
        else
        {
            orionAnim.SetBool("Walk", false);
            runParticals.SetActive(false);
        }
            

    }

    private void FixedUpdate()
    {
        if(inTutorial == false)
        {
            Move();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saddle")) 
        {
            if(!isMounted)
            {
                space.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    FindAnyObjectByType<AudioManager>().Play("Jump");
                    gambleWalk.enabled = true;
                    StartCoroutine(Mount());
                    gambleTower.enabled = false;
                    healthBar.SetActive(false);
                    range.SetActive(false);
                    gamblehealth.enabled = false;
                  

                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Saddle"))
        {
            space.SetActive(false);
        }
    }
    IEnumerator Mount()
    {
        yield return new WaitForSeconds(0.005f);
        gameObject.transform.SetParent(saddle);
        GameManager.instance.EnableAllMeshMounted();
        gameObject.SetActive(false);
        isMounted = true;
        yield return new WaitForSeconds(0.005f);
    }
    public void WalkAnim()
    {
         var hitKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow);

            if (hitKey == true && !isMounted)
            {
                // Activate the animation
                orionAnim.SetBool("Walk", true);
                runParticals.SetActive(true);

            }
            else
            {
                // Activate the animation
                orionAnim.SetBool("Walk", false);
                runParticals.SetActive(false);
            }
    }
    public void AttackAnim()
    {
        if (Input.GetMouseButtonDown(0) && !isMounted)
        {
            
            sword.SetBool("Swing", true);
            FindAnyObjectByType<AudioManager>().Play("HatThrow");
            //HitPartical();
            StartCoroutine(ParticalSlash());
        }
        else
        {
            sword.SetBool("Swing", false);
        }
    }
    IEnumerator ParticalSlash()
    {
        yield return new WaitForSeconds(0.15f);
        hit.Play();
    }
    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(input.ToIso2(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        // Calculate the desired movement direction
        Vector3 desiredMovement = transform.forward * input.normalized.magnitude * speed * Time.deltaTime;

        // Perform a movement raycast to check for collisions
        RaycastHit hit;
        if (rb.SweepTest(desiredMovement.normalized, out hit, desiredMovement.magnitude))
        {
            // Check if the collided object's collider is not a trigger
            if (!hit.collider.isTrigger)
            {
                // Adjust the movement direction to prevent clipping
                desiredMovement = Vector3.ProjectOnPlane(desiredMovement, hit.normal);
            }
        }

        // Move the Rigidbody to the adjusted position
        rb.MovePosition(rb.position + desiredMovement);
    }

    
}

public static class HelpersOrion
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso2(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}