using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Project.Utils;

public class ShootingEnemy : DamageableEnemy
{
    [Header("Shooting")]
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _reloadTime;

    private float _shootTimer;
    private bool _shootAtPlayer;
    private Weapon _weapon;

    protected override void Setup()
    {
        base.Setup();

        _weapon = new Weapon(3, 1);
        _shootAtPlayer = false;
        //_shootAtPlayer = Random.value < 0.5f;
        _shootTimer = 0;
    }

    protected override void Behavior()
    {
        _shootTimer -= Time.deltaTime;

        if (_shootTimer <= 0)
        {
            _shootTimer = _weapon.Bullets <= 0 ? _reloadTime : _shootSpeed;

            Shoot();
        }
    }

    private void Shoot()
    {
        if (_weapon == null)
        {
            return;
        }

        if(_weapon.UseBullet())
        {
            Instantiate(_bulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation, null);
        }
        else
        {
            _weapon.Reload();
        }
    }
}
