using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem : MonoBehaviour
{
    private PlayerInput inputAction;
    private InputActionMap inputMap;
    private InputAction attackInput;

    [SerializeField] float damage;
    [SerializeField] HeathSystem heathSystem;
    [SerializeField] bool canAtack;
    [SerializeField] List<GameObject> targets;
    protected bool isAtacking;
    /// <summary>
    /// Este codigo e somente para o player, o do npc esta dentro do codigo do npc
    /// </summary>

    void Start()
    {
        canAtack = false;
        targets = new List<GameObject>();
        isAtacking = false;
        inputAction = GetComponent<PlayerInput>();
        inputMap = inputAction.currentActionMap;
        attackInput = inputMap["Attack"];
    }


    void Update()
    {
        // Adicionar clique de ataque
        if (canAtack && attackInput.IsPressed())
        {
            //  animator.SetBool("Atack", atack);
            StartCoroutine(cooldown(1.0f));
        }
    }

    // Serve para indicar que se ele atacar, algo vai receber dano
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canAtack = true;
            targets.Add(other.gameObject);
        }
    }

    // Serve para indicar que se ele atacar, este não ira receber o danos
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canAtack = false;
            targets.Remove(other.gameObject);
        }
    }

    // Serve para evitar o player de expanar o botão de ataque, o time precisa ser o tempo da animação
    private IEnumerator cooldown(float time)
    {
        if (!isAtacking)
        {
            isAtacking = true;
            yield return new WaitForSecondsRealtime(time);
            // Só dara dano no final do ataque, nos objetos que tiver na lista targets
            if (canAtack)
            {
                foreach (GameObject target in targets)
                {
                    heathSystem = target.GetComponent<HeathSystem>();
                    // Verifica se esta vivo
                    if (heathSystem.alive())
                    {
                        // se estiver vivo dara o dano
                        heathSystem.dealDamage(damage);
                    }
                }
            }
            // marca que o ataque funcionou
            isAtacking = false;
        }
    }
}
