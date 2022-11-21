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
    // Caso falso, não vai colocar o osso capturado na tela, caso verdadeiro, colocara o proximo da lista.
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

    void Update()
    {
        controlCount = gameControllers.Count;
        boneCount = bone.bones.Count;

        if (i <= boneCount && canNext && boneCount != 0)
        {
            pauseGame();
            
            // Coloca o botão de fechar
            buttonObject.SetActive(true);
            if (i > 0)
            {
                // Escode o osso anterior
                bone.bones[i - 1].transform.position = hide;
            }
            // Verifica se tem mais osso
            if (i != boneCount)
            {
                // se tiver ele mostra este osso, e coloca nele a função que permite girar
                bone.bones[i].transform.position = show;
                bone.bones[i].AddComponent<UIBoneViewer>();
            }
            canNext = false;
            i++;
        }
    }

    // Quando o jogador fechar o visualizador do osso
    public void clickHandler()
    {
        canNext= true;
        buttonObject.SetActive(false);
        ResumeGame();
    }

    /// <summary>
    /// Pausar e resumir o jogo, esta configurado para esconder e mostrar os ossos
    /// </summary>
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
