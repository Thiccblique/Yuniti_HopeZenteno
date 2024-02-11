using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneOnSetTime : MonoBehaviour
{
    public LoadingScreen loadingScreen;
    public float changetime;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changetime -= Time.deltaTime;

        if (changetime <= 0)
        {
            loadingScreen.LoadScene(sceneName);
        }
    }

    public void SceneOnPress()
    {
        SceneManager.LoadScene(sceneName);
    }
}
