using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    private Animator anim;

    public bool isOpen = true;
    public bool isClosed = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void HUDController()
    {
       

       
       

    }


      


    
    public void OpenHUD()
    {
        if (isOpen)
        {
            anim.SetBool("Close", true);
            isOpen = false;
            anim.SetBool("Still", true);
        }
       
        if (!isOpen)
        {
            anim.SetBool("Open", true);
            isOpen = true;
            anim.SetBool("StayOpen", true);
        }
       

    }
    public void CloseHUD()
    {
       
    }
    public void StillHUD()
    {
        anim.SetBool("Still", true);
    }
}
