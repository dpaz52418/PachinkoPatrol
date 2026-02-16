using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   
    public static SceneLoader Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //you can load a scene by its name
    public void LoadThisScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    //or load a scene by its index in the build settings
    public void LoadThisScene(int index){
        SceneManager.LoadScene(index);
    }

    //if your scenes are in the correct order
    //you can also load the next scene like this
    public void LoadNextScene(int index){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //you can also reload the current scene using the same method function
    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //or 
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
