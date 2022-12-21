using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnGround : MonoBehaviour
{
    [SerializeField] bool isOnGround;

    public bool GetBoolGround()
    {
        return isOnGround;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            isOnGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            isOnGround = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            isOnGround = true;
        }
    }

    public void onClick()
    {
        Debug.Log("Apertou!!!");
    }
}
