using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [Header("TargetObject")]
    public GameObject targetGameObject;
    
    [Header("Animations")]
    public string nextAnimationName = "Next";
    public string backAnimationName = "Back";

    [Header("Buttons")]
    public GameObject nextButton;
    public GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNextAnimation()
    {
        Animator animator = targetGameObject.GetComponent<Animator>();
        animator.Play(nextAnimationName);
        nextButton.SetActive(false); 
        backButton.SetActive(false);
    }
    public void PlayBackAnimation()
    {
        Animator animator = targetGameObject.GetComponent<Animator>();
        animator.Play(backAnimationName);
        nextButton.SetActive(false);
        backButton.SetActive(false);
    }


    public void ReactivateButtons()
    {
        nextButton.SetActive(true);
        backButton.SetActive(true);
    }

}
