using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    protected static PlayerScript instance;
    [SerializeField] GameObject playerCamera;
    // Start is called before the first frame update

    public static PlayerScript getInstance()
    { 
        return instance; 
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(playerCamera);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(playerCamera);

    }
}
