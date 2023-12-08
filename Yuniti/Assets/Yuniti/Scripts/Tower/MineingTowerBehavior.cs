using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineingTowerBehavior : MonoBehaviour
{
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Animations());
    }

    IEnumerator Animations()
    {
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("run", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("run", false);
       

    }
}
