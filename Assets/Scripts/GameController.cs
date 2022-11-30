using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    protected static GameController instance;
    [SerializeField] int controlCount,boneCount, i;
    protected Vector3 hide;
    protected Vector3 show;
    // Caso falso, não vai colocar o osso capturado na tela, caso verdadeiro, colocara o proximo da lista.
    [SerializeField] bool canNext;
    [SerializeField] Button button;
    [SerializeField] GameObject buttonObject;
    [SerializeField] List<GameObject> bone;
    [SerializeField] List<GameObject> gameControllers;
    [SerializeField] Camera cameraMan;

    public static GameController getInstance()
    {
        return instance;
    }

    void Start()
    {
        bone = new List<GameObject>();
        i = 0;
        canNext = true;
        button.onClick.AddListener(clickHandler);
        buttonObject.SetActive(false);
        hide = new Vector3(0, 0, -1);
        show = new Vector3(0, 0, 1);

        if( instance!= null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        controlCount = gameControllers.Count;
        boneCount = bone.Count;

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = cameraMan.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("Bone"))
                {
                    raycastHit.collider.transform.position = hide;
                    bone.Add(raycastHit.collider.gameObject);
                }
            }
        }

        if (boneCount != 0 && i < boneCount && canNext)
        {
            pauseGame();
            
            canNext = false;
            // Coloca o botão de fechar
            buttonObject.SetActive(true);
            
            // Verifica se tem mais osso
            if (i != boneCount)
            {
                // se tiver ele mostra este osso, e coloca nele a função que permite girar
                bone[i].transform.position = show;
                bone[i].AddComponent<UIBoneViewer>();
            }
            i++;
        }
    }

    // Quando o jogador fechar o visualizador do osso
    public void clickHandler()
    {
        canNext= true;
        buttonObject.SetActive(false);
        if (i > 0)
        {
            // Escode o osso anterior
            bone[i - 1].transform.position = hide;
        }
        ResumeGame();
    }

    /// <summary>
    /// Pausar e resumir o jogo, esta configurado para esconder e mostrar os ossos
    /// </summary>
    private void pauseGame()
    {
        int j = 0;
        Time.timeScale = 0;
        while (controlCount > j) {
            gameControllers[j].SetActive(false);
            j++;
        }
        j = 0;
    }

    private void ResumeGame()
    {
        int j = 0;
        Time.timeScale = 1;
        while (controlCount > j)
        {
            gameControllers[j].SetActive(true);
            j++;
        }
        j = 0;
    }
}
