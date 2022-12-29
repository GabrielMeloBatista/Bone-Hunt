using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string exitName;
    public LoadingScreenController loadingScreenController;

    void Start()
    {
        loadingScreenController = FindObjectOfType<LoadingScreenController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastExitName", exitName);
            loadingScreenController.LoadLevel(sceneToLoad);
        }
    }
}
