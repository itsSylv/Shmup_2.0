using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnPos;

    private float _timer;
    private bool _isShooting = false;
    
    private void Start()
    {
        _timer = 0;
        InputManager.Main.OnFire += Shoot;
    }

    private void Shoot()
    {
        _isShooting = !_isShooting;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            return;
        }

        if (_isShooting)
        {
            GameObject bullet = Instantiate(_bullet, _bulletSpawnPos.position, Quaternion.identity, null);
            bullet.GetComponent<Bullet>().SetDamage(_damage);
            _timer = _cooldown;
        }
    }

    public void UpdateDamage(int num)
    {
        _damage += num;
    }

    public void UpdateCooldown(int num)
    {
        _cooldown -= num;
    }
}
