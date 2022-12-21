using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    protected static GameController instance;

    float x, y, z;
    Quaternion viwerRotation;

    protected Vector3 hide;
    protected Vector3 show;

    protected GameObject viewing;
    [SerializeField] int controlCount, boneCount, i;
    // Caso falso, não vai colocar o osso capturado na tela, caso verdadeiro, colocara o proximo da lista.
    [SerializeField] bool canNext;
    // Quando se esta visualizando o osso
    [SerializeField] bool isViewer;
    [SerializeField] bool isShopping = false;

    [SerializeField] Button button;
    [SerializeField] GameObject buttonObject;
    [SerializeField] List<GameObject> gameControllers;
    [SerializeField] Camera cameraMan;
    [SerializeField] Camera boneCamera;

    public bool isTemple;

    [SerializeField] List<GameObject> bone;

    public static GameController getInstance()
    {
        return instance;
    }

    public List<GameObject> getBone()
    {
        return bone;
    }

    public GameObject getViewerBone()
    {
        if (isViewer)
        {
            return viewing;
        }
        else
        {
            return bone[i];
        }
    }

    public bool getIsShopping()
    {
        return isShopping;
    }

    public Camera getCamera()
    {
        return boneCamera;
    }

    public void HandleStore()
    {
        isShopping = !isShopping;
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        bone = new List<GameObject>();
        i = 0;
        button.onClick.AddListener(clickHandler);
        buttonObject.SetActive(false);
        hide = new Vector3(0, 0, -1);
        show = new Vector3(0, 0, 1);
        canNext = true;
        isViewer = false;
        cameraMan = Camera.main;
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
                if (raycastHit.collider.CompareTag("Viewer") && !isViewer)
                {
                    viewing = raycastHit.collider.gameObject;
                    x = viewing.transform.position.x;
                    y = viewing.transform.position.y;
                    z = viewing.transform.position.z;
                    viwerRotation = viewing.transform.rotation;
                    viewing.transform.position = show;
                    viewing.AddComponent<UIBoneViewer>();
                    isViewer = true;
                    pauseGame();
                    buttonObject.SetActive(true);
                }
                if (raycastHit.collider.CompareTag("Shop"))
                {
                    isShopping = true;
                    raycastHit.collider.gameObject.GetComponent<Merchant>().HandleStore();
                    pauseGame();
                }
            }
        }

        isTemple = SceneManager.GetActiveScene().name.Equals("InsideTempla");

        if (boneCount != 0 && i < boneCount && canNext && isTemple)
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
        if (isViewer)
        {
            viewing.transform.position = new Vector3(x, y, z);
            viewing.transform.rotation = viwerRotation;
            viewing.GetComponent<UIBoneViewer>().closeViewer();

            isViewer = false;
        }
        else
        {
            canNext = true;
            if (i > 0)
            {
                // Escode o osso anterior
                bone[i - 1].transform.position = hide;
                try
                {
                    bone[i - 1].GetComponent<UIBoneViewer>().closeViewer();
                }
                catch
                {
                    isViewer = false;

                }
            }
        }
        buttonObject.SetActive(false);
        ResumeGame();
    }

    /// <summary>
    /// Pausar e resumir o jogo, esta configurado para esconder e mostrar os ossos
    /// </summary>
    public void pauseGame()
    {
        int j = 0;
        Time.timeScale = 0;
        while (controlCount > j)
        {
            gameControllers[j].SetActive(false);
            j++;
        }
        j = 0;
    }

    public void ResumeGame()
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
