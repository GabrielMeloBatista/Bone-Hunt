using System.Collections;
using System.Threading;
using UnityEngine;

public class Projectale : MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        StartCoroutine(timeOut(20.0f));
    }

    void Update()
    {
        // Move the projectile in the direction it is facing
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy the projectile when it collides with something
        Destroy(gameObject);
    }

    protected IEnumerator timeOut(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}

