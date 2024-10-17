using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableEnemy : Enemy, IDamageable
{
    [SerializeField] private int _hitPoints;
    [SerializeField] [Range(0, 100)] private int _powerupDropChance;

    protected override void Setup()
    {
        base.Setup();
        
        // TODO: Get a random powerup from the powerup manager
    }

    public void Damage(int damage)
    {
        _hitPoints -= damage;

        if (_hitPoints <= 0)
        {
            TrySpawnPowerup();
            OnDeath();
        }
    }

    private void TrySpawnPowerup()
    {
        if (_powerupDropChance > 0 && Random.Range(1, 100) <= _powerupDropChance)
        {
            // TODO: spawn powerup
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
