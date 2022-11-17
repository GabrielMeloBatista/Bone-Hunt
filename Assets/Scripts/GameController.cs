using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    protected int controlCount,boneCount, i, j;
    protected Vector3 hide;
    protected Vector3 show;
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
        hide = new Vector3(0, 0, -1);
        show = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        controlCount = gameControllers.Count;
        boneCount = bone.bones.Count;

        if (i < boneCount && canNext)
        {
            pauseGame();
            buttonObject.SetActive(true);
            if (i > 0)
            {
                bone.bones[i - 1].transform.position = hide;
            }
            bone.bones[i].transform.position = show;
            canNext = false;
            i++;
        }
        else if (i == boneCount && canNext && boneCount != 0)
        {
            pauseGame();
            buttonObject.SetActive(true);
            bone.bones[i - 1].transform.position = hide;
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
        while (controlCount > j) {
            gameControllers[j].SetActive(false);
            j++;
        }
        j = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        while (controlCount > j)
        {
            gameControllers[j].SetActive(true);
            j++;
        }
        j = 0;
    }
}
