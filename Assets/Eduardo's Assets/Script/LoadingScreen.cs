using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject loadingScreenUI;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private float minimumWaitTime = 2f;

    private void Start()
    {
        // Show the loading screen UI
        loadingScreenUI.SetActive(true);

        // Start loading the scene asynchronously
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        float startTime = Time.time;

        while (!operation.isDone)
        {
            // Update the progress bar based on the loading progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.SetRadialValue(progress);

            yield return null;
        }

        // Calculate the actual loading time
        float loadingTime = Time.time - startTime;

        // Wait for the remaining time to meet the minimum wait time
        float remainingTime = Mathf.Max(minimumWaitTime - loadingTime, 0f);
        yield return new WaitForSeconds(remainingTime);

        // Switch to the loaded scene
        SceneManager.LoadScene(sceneToLoad);

    }
}
