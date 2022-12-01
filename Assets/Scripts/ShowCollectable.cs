using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCollectable : MonoBehaviour
{
    [SerializeField] Collectable collectable;
    [SerializeField] List<GameObject> viewBone = new List<GameObject>();

    void Start()
    {
        collectable = Collectable.getInstance();

        for (int i = 0; i < transform.childCount; i++)
        {
            viewBone.Add(transform.GetChild(i).gameObject);
        }
        foreach (GameObject m in viewBone)
        {
            if (!collectable.m_Collectables.Contains(m.name))
                m.SetActive(false);
        }
    }


    /*void Update()
    {
        
    }*/
}
