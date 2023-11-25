using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void HUDController()
    {
        if (HUD != null)
        {
            Animator anim = HUD.GetComponent<Animator>();
            if(anim != null)
            {
                bool isOpen = anim.GetBool("open");
              
                anim.SetBool("open", !isOpen);
            }
        }
       
 
    }
}
