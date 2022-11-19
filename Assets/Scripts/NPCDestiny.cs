using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDestiny : MonoBehaviour
{
    [SerializeField] int pivotPoint;
    private Vector3 positionA, positionB, positionC, positionD;

    private void Start()
    {
        positionA = new Vector3(-33f, 17f, 17f);
        positionB = new Vector3(-20f, 17f, 17f);
        positionC = new Vector3(-19f, 34f, 62f);
        positionD = new Vector3(-25f, 24f, 29f);
    }

    private void nextPosition()
    {
        if (pivotPoint == 3)
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
        if (other.CompareTag("NPC"))
        {
            switch (pivotPoint)
            {
                case 0:
                    this.gameObject.transform.position = positionA;
                    nextPosition();
                    break;
                case 1:
                    this.gameObject.transform.position = positionB;
                    nextPosition();
                    break;
                case 2:
                    this.gameObject.transform.position = positionC;
                    nextPosition();
                    break;
                case 3: 
                    this.gameObject.transform.position = positionD;
                    nextPosition();
                    break;
            }
            
        }
    }
}
