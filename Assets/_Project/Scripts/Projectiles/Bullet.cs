using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _destroyTimeSeconds;
    private float _damage;

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
}
