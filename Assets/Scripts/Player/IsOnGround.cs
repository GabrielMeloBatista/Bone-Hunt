using UnityEngine;

public class IsOnGround : MonoBehaviour
{
    [SerializeField] bool isOnGround;

    public bool GetBoolGround()
    {
        return isOnGround;
    }

    void Update()
    {
        // Raycast down from the player's feet to check for the ground
        RaycastHit hit;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint - Vector3.up * 0.1f;
        if (Physics.Raycast(startPoint, endPoint, out hit))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }
}
