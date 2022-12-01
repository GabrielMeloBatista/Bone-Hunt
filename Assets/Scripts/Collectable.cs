using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected static Collectable instance;
    public static Collectable getInstance() { return instance; }

    [SerializeField] List<string> m_Collectables = new List<string>();

    [SerializeField] GameController gameController = GameController.getInstance();

    public List<string> getCollected() { return m_Collectables; }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        foreach (GameObject m in gameController.getBone())
        {
            if (!m_Collectables.Contains(m.name))
                m_Collectables.Add(m.name);
        }
    }
}
