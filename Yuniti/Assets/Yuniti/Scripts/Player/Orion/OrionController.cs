using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class OrionController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 input;

    public bool isMounted = true;
    public GameObject onOrion;
    public Transform saddle;
    void Start()
    {
        isMounted = false;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saddle")) 
        {
            if(!isMounted)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameObject.transform.SetParent(saddle);
                    onOrion.SetActive(true);
                    gameObject.SetActive(false);
                    isMounted = true;
                }
            }
        }
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
        rb.MovePosition(transform.position + transform.forward * input.normalized.magnitude * speed * Time.deltaTime);
    }
}

public static class HelpersOrion
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso2(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}