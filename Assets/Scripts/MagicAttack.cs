using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MagicAttack : MonoBehaviour
{
    [SerializeField] GameObject[] thePlayer;
    protected float distance = 0.0f;
    protected bool isAttacking = false;
    float dificulty = 1.0f;

    [SerializeField] GameObject projectilePrefab;

    protected Vector3 playerPosition;
    protected Vector3 gunPosition;
    [SerializeField] GameObject gun;

    protected Quaternion gunRotation;
    void Start()
    {
        thePlayer = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if (GetComponent<HeathSystem>().alive())
        {
            playerPosition = thePlayer[0].transform.position;
            gunPosition = gun.transform.position;
            gun.transform.LookAt(playerPosition);
            gunRotation = gun.transform.rotation;

            distance = Vector3.Distance(gunPosition, playerPosition);
            if (distance < 20)
            {
                StartCoroutine(routine: routine(dificulty));
            }
        }
    }

    IEnumerator routine(float time)
    {
        if(!isAttacking)
        {
            isAttacking = true;
            yield return new WaitForSecondsRealtime(time);
            Debug.Log(gunRotation);
            Instantiate(projectilePrefab, gunPosition, gunRotation);
            isAttacking = false;
        }
    }
}
