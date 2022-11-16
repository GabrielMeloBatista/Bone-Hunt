using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    protected int i, j;
    [SerializeField] BonePicker bone;
    [SerializeField] bool canNext;
    [SerializeField] Button button;
    [SerializeField] GameObject buttonObject;
    [SerializeField] List<GameObject> gameControllers;

    private void Start()
    {
        i = 0;
        j = 0;
        canNext = true;
        button.onClick.AddListener(clickHandler);
        buttonObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (i < bone.bones.Count && canNext)
        {
            pauseGame();
            buttonObject.SetActive(true);
            if (i > 0)
            {
                bone.bones[i - 1].transform.position = new Vector3(0, 0, -1);
            }
            bone.bones[i].transform.position = new Vector3(0, 0, 1);
            canNext = false;
            i++;
        }
        else if (i == bone.bones.Count && canNext && bone.bones.Count != 0)
        {
            pauseGame();
            buttonObject.SetActive(true);
            bone.bones[i - 1].transform.position = new Vector3(0, 0, -1);
            canNext = false;
            i++;
        }
    }

    public void clickHandler()
    {
        canNext= true;
        buttonObject.SetActive(false);
        ResumeGame();
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
        while (gameControllers.Count> j) {
            gameControllers[j].SetActive(false);
            j++;
        }
        j = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        while (gameControllers.Count > j)
        {
            gameControllers[j].SetActive(true);
            j++;
        }
        j = 0;
    }
}
