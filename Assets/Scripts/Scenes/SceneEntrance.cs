using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    [SerializeField] string lastExitName;
    private PlayerScript playerInstance;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = PlayerScript.getInstance();
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            playerInstance.transform.position = transform.position;
            playerInstance.transform.eulerAngles= transform.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
