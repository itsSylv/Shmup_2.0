using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _destroyTimeSeconds;
    private int _damage;

    private void Start()
    {
        Destroy(gameObject, _destroyTimeSeconds);
    }

    private void Update()
    {
        transform.position += transform.up * (_bulletSpeed * Time.deltaTime);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if what we hit is damageable
        if (other.TryGetComponent(out IDamageable iDamageable))
        {
            iDamageable.Damage(_damage);
        }
        
        Destroy(gameObject);
    }
}
