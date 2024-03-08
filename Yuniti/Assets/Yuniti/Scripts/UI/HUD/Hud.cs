using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public static Hud instance;

    public GameObject HUD;
    public bool isOpen = true;
    public GameObject normalSpeed;
    public GameObject doubleSpeed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (HUD != null)
            {

                Animator anim = HUD.GetComponent<Animator>();
                if (anim != null)
                {
                    isOpen = anim.GetBool("open");

                    anim.SetBool("open", !isOpen);

                }
            }
        }
    }

    public void HUDController()
    {
       
       
        if (HUD != null)
        {
            
            Animator anim = HUD.GetComponent<Animator>();
            if(anim != null)
            {
                isOpen = anim.GetBool("open");
              
                anim.SetBool("open", !isOpen);
                
            }
        }
       
 
    }

    public void AudioSelect()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
    }

    public void DoubleSpeed()
    {
        Time.timeScale = 2f;
        normalSpeed.SetActive(false);
        doubleSpeed.SetActive(true);
    }

    public void ResetSpeed()
    {
        Time.timeScale = 1f;
        normalSpeed.SetActive(true);
        doubleSpeed.SetActive(false);
    }
}
