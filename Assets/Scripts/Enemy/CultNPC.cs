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
    [SerializeField] GameObject thePlayer;
    [SerializeField] float damage;
    protected Vector3 destination;
    protected Vector3 playerPosition;
    protected Vector3 cultPositon;
    protected int isWalkingHash;
    protected int isRunningHash;
    protected float speed;
    protected float distance;

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        distance = 0.0f;
        isAtacking = false;
    }

    void Update()
    {
        if (this.GetComponent<HeathSystem>().alive())
        {

            // Velocidade do objeto
            speed = theAgent.velocity.sqrMagnitude;

            // Verifica se esta andando
            isWalking = speed > 0.5f;
            // Verifica se esta correndo
            isRunning = speed >= 3.0f;

            // posi��o do player
            playerPosition = thePlayer.transform.position;
            // posi��o do enimigo
            cultPositon = this.transform.position;

            // Decide se ira caminha aleatoriamente ou ir ao player
            if (willAtack && distance < 20.0f && distance != 0.0f)
            {
                destination = thePlayer.transform.position;
            }
            else
            {
                destination = theDestination.transform.position;
            }

            // define o destino
            theAgent.SetDestination(destination);


            distance = Vector3.Distance(cultPositon, playerPosition);

            animator.SetBool(isWalkingHash, isWalking);
            animator.SetBool(isRunningHash, isRunning);

            if (canAtack)
            {
                //  animator.SetBool("Atack", atack);
                StartCoroutine(cooldown(1.0f));
            }
        }
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
        if (!isAtacking)
        {
            isAtacking = true;
            yield return new WaitForSecondsRealtime(time);
            if (canAtack)
            {
                thePlayer.GetComponent<HeathSystem>().dealDamage(damage);
            }
            isAtacking = false;
        }
    }
}
