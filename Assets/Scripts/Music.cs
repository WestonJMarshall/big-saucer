using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public GameObject musicPrefab;
    private GameObject sceneMusic;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        sceneMusic = GameObject.FindGameObjectWithTag("Music");
        if (sceneMusic == null)
        {
            sceneMusic = Instantiate(musicPrefab);
            DontDestroyOnLoad(sceneMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
