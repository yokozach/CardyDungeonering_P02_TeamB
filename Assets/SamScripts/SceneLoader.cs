using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("Going to Scene: " + sceneName);
        CentralManager _centralManager = (CentralManager)FindObjectOfType(typeof(CentralManager));
        StartCoroutine(_centralManager._introFade.FadePanelIn());
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
