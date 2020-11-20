using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startUI;
    public GameObject controlsUI;

    public void Awake()
    {
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Controls()
    {
        startUI.SetActive(false);
    }

    public void BackToStart()
    {
        startUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
