using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CultNPC : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject theDestination;
    NavMeshAgent theAgent;
    [SerializeField] bool canAtack;
    [SerializeField] bool willAtack;
    protected bool isAtacking;
    private bool isWalking;
    private bool isRunning;
    [SerializeField] GameObject[] thePlayer;
    [SerializeField] float damage;
    protected Vector3 destination;
    protected Vector3 playerPosition;
    protected Vector3 cultPositon;
    protected int isWalkingHash;
    protected int isRunningHash;
    protected float speed;
    protected float distance;
    protected bool isOnDestiny= false;
    [SerializeField] bool isPlayerTarget = false;

    public void isOnDestinyHandler()
    {
        isOnDestiny = true;
    }

    void Start()
    {
        thePlayer = GameObject.FindGameObjectsWithTag("Player");
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        distance = 0.0f;
        isAtacking = false;
        destination = theDestination.transform.position;
        setNPCDestiny();
    }

    void Update()
    {
        if (this.GetComponent<HeathSystem>().alive())
        {
            // Distancia do NPC com o player
            distance = Vector3.Distance(cultPositon, playerPosition);

            // Se for atacar e a distancia for de 20, então o player vira o alvo
            if (willAtack && distance < 20.0f && distance != 0.0f)
            {
                isPlayerTarget = true;
                destination = thePlayer[0].transform.position;
                setNPCDestiny();
            }
            else
            {
                isPlayerTarget= false;
            }

            // Caso o player não for o alvo, vai seguir o caminho padrão
            if(!isPlayerTarget)
            {
                destination = theDestination.transform.position;
                setNPCDestiny();
            }

            // Se chegou no destino, vai para o proximo destino
            if(isOnDestiny)
            {
                isOnDestiny= false;
                destination = theDestination.transform.position;
                setNPCDestiny();
            }

            animator.SetBool(isWalkingHash, isWalking);
            animator.SetBool(isRunningHash, isRunning);

            // Se for atacar já conta o cooldown
            if (canAtack)
            {
                //  animator.SetBool("Atack", atack);
                StartCoroutine(cooldown(1.0f));
            }
        }
    }

    protected void setNPCDestiny()
    {
        // Velocidade do objeto
        speed = theAgent.velocity.sqrMagnitude;

        // Verifica se esta andando
        isWalking = speed > 0.5f;
        // Verifica se esta correndo
        isRunning = speed >= 3.0f;

        // posição do player
        playerPosition = thePlayer[0].transform.position;
        // posição do enimigo
        cultPositon = this.transform.position;

        // Decide se ira caminha aleatoriamente ou ir ao player


        // define o destino
        theAgent.SetDestination(destination);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canAtack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canAtack = false;
        }
    }

    private IEnumerator cooldown(float time)
    {
        // Se não estiver atacando ele ataca
        if (!isAtacking)
        {
            // Esta parte e para evitar spannar ataque
            isAtacking = true;
            // ataca
            thePlayer[0].GetComponent<HeathSystem>().dealDamage(damage);
            
            // Este e o cooldown
            yield return new WaitForSecondsRealtime(time);

            //Apos o cooldown terminar ele avisa que pode atacar novamente se possivel
            isAtacking = false;
        }
    }
}
