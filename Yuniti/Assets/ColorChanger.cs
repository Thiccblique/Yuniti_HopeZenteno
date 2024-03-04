using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material[] skyMaterials;

    public GameObject[] skyDomes;

    public GameObject[] ppVolumes;

    public GameObject[] objectSwitch;

    public float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        ppVolumes = GameObject.FindGameObjectsWithTag("P.P");

        RenderSettings.skybox = skyMaterials[0];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
