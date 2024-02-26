using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudActivator : MonoBehaviour
{
    public TutorialHud tutorialHud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tutorialHud.buyTower == false)
        {
            tutorialHud.startedHud = true;
            tutorialHud.buyTower = true;
        }

    }
}
