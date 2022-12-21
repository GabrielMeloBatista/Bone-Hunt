using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDestiny : MonoBehaviour
{
    [SerializeField] int pivotPoint;
    [SerializeField] List<Vector3> position;
    [SerializeField] GameObject[] targets;

    private void Start()
    {
        // Gambiarra, muda salva o ponte de destino do personagem
        position = new List<Vector3>
        {
            new Vector3(-33f, 17f, 17f),
            new Vector3(-20f, 17f, 17f),
            new Vector3(-19f, 34f, 62f),
            new Vector3(-25f, 24f, 29f)
        };

        // Buscar os NPCs
        targets = GameObject.FindGameObjectsWithTag("NPC");
    }
    
    // Metodo que garante que a posição esteja dentro do numero possivel.
    private void nextPosition()
    {
        if (pivotPoint == position.Count-1)
        {
            pivotPoint = 0;
        }
        else
        {
            pivotPoint++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Caso um npc tenha chegado ao destino, move o destino para o proxima posição
        if (other.CompareTag("NPC"))
        {           
            this.gameObject.transform.position = position[pivotPoint];
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<CultNPC>().isOnDestinyHandler();
            }
            nextPosition();
        }
    }
}
