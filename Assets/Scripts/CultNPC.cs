using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CultNPC : MonoBehaviour
{
    Animator animator;
    public GameObject theDestination;
    NavMeshAgent theAgent;
    public static bool canAtack;
    private bool isWalking;
    private bool isRunning;
    public GameObject thePlayer;
    protected Vector3 destination;
    protected int isWalkingHash;
    protected int isRunningHash;
    protected float speed;

    void Start()
    {
        theAgent= GetComponent<NavMeshAgent>();
        animator= GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    private void Update()
    {
        speed = theAgent.velocity.sqrMagnitude;

        isWalking = speed > 0.5f;
        isRunning = speed >= 3.0f;

        if (canAtack)
        {
            destination = thePlayer.transform.position;
        }
        else
        {
            destination = theDestination.transform.position;
        }

        theAgent.SetDestination(destination);
        animator.SetBool(isWalkingHash, isWalking);
        animator.SetBool(isRunningHash, isRunning);
    }
}
