using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuFix : MonoBehaviour
{
    [SerializeField] CentralManager centralManager;
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        centralManager._playerController.paused = true;
        centralManager._musicPlayer.SetMuffled(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        centralManager._playerController.paused = false;
        centralManager._musicPlayer.SetMuffled(false);
        Time.timeScale = 1f;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
