using System;
using UnityEngine;

public class ShootableObject : MonoBehaviour, IDestructible
{
    [SerializeField] int maxHealth;
    private int _health;

    public void TakeDamage(int amount)
    {
        _health = Math.Max(0, _health - amount);

        if (_health <= 0)
        {
            Die();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Die()
    {
        Destroy(gameObject);
    }
}
