using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    private Animator animator;
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        speed = Random.Range(0.2f, 1.3f);        
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = speed;
    }
}
