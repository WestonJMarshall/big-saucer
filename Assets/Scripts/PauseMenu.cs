using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool IsGamePaused = true;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //escape key and 'P' are set to pause the game
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            pauseMenuUI.SetActive(IsGamePaused);
            IsGamePaused = !IsGamePaused;
        }
    }

    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
