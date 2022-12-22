using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathSystem : MonoBehaviour
{
    [SerializeField] float maxHealth, minHealth, health;
    [SerializeField] bool isAlive;
    protected int isWalkingHash;
    protected int isRunningHash;
    Animator animator;

    void Start()
    {
        maxHealth = 100.0f;
        health = maxHealth;
        minHealth = 0.0f;
        isAlive = true;
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }
    void Update()
    {
        if (health <= minHealth && isAlive)
        {
            // Menu de gameover aqui
            Debug.Log("Dead");
            isAlive= false;
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
        }
    }

    public void dealDamage(float damage)
    {
        if (isAlive)
        {
            health-=damage;
        }
    }

    public void upgradeHeath(float hearts)
    {
        maxHealth += hearts;
    }

    public void heal(float hearts)
    {
        health += hearts;
    }

    public float getHeath()
    {
        return health;
    }

    public float getMaxHeath()
    {
        return maxHealth;
    }

    public bool alive()
    {
        return isAlive;
    }
}
