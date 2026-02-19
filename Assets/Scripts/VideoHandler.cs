using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component not found on this GameObject.");
            return;
        }
        mainCamera = Camera.main;
        videoPlayer.targetCamera = mainCamera;
        if (mainCamera == null)        {
            Debug.LogError("Main Camera not found in the scene.");
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
        videoPlayer.targetCamera = mainCamera;
        if (mainCamera == null)        {
            Debug.LogError("Main Camera not found in the scene.");
            return;
        }
    }
}
