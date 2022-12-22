using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Translate()
    {

    }
    public void Creditos()
    {
        SceneManager.LoadScene(4);
    }
    public void Config()
    {
        SceneManager.LoadScene(3);
    }
    public void Return()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Debug.Log("Close Game");
        Application.Quit();
    }
    public void ComoJogar()
    {
        SceneManager.LoadScene(5);
    }
}
