using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este codigo vai olhar o osso coletado e salva-lo na lista para uso futuro
/// </summary>

public class BonePicker : MonoBehaviour
{
    public List<GameObject> bones = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bone"))
        {
            other.transform.position = new Vector3(0, 0, -1);
            bones.Add(other.gameObject);
        }
    }
}
