using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 20;
    private float health;
    public GameObject deathEffect;
    public float damage = 10f;

    private void Start()
    {
        health = maxHealth;
        print(health.ToString());
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);

        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect);
            Destroy(effect, 5);
        }
    }
}