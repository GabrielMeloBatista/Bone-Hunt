using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.ComponentModel;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    public Canvas loadingScreenCanvas;
    public GameObject loadingScreenPanel;
    public TMP_Text loadingMessageText;
    public Slider loadingBar;
    public float loadingProgress;
    UnityEngine.AsyncOperation asyncOperation;

    void Start()
    {
        loadingScreenCanvas = GameObject.Find("LoadingScreenCanvas").GetComponent<Canvas>();
        loadingScreenPanel = GameObject.Find("LoadingScreenPanel");
        loadingMessageText = GameObject.Find("LoadingMessageText").GetComponent<TMP_Text>();
        loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();

        HideLoadingScreen();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideLoadingScreen();
    }

    void ShowLoadingScreen()
    {
        loadingScreenCanvas.enabled = true;
        loadingBar.value = 0;
    }

    void HideLoadingScreen()
    {
        loadingScreenCanvas.enabled = false;
    }

    void UpdateLoadingScreen(float progress)
    {
        loadingProgress = progress;
        loadingBar.value = loadingProgress;
        loadingMessageText.text = (int)(loadingProgress * 100) + "%";
    }

    public void LoadLevel(string levelName)
    {
        ShowLoadingScreen();
        StartCoroutine(LoadSceneAsync(levelName));
        // SceneManager.LoadScene(levelName);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            Debug.Log("Loading progress: " + asyncOperation.progress);

            UpdateLoadingScreen(asyncOperation.progress);
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
