using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Project.Utils;

public class ShootingEnemy : DamageableEnemy
{
    [Header("Movement")]
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _arrivalTimeSeconds;
    [SerializeField] private SpriteRenderer _sprite;

    [Header("Shooting")]
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _reloadTime;

    private float _shootTimer;
    private bool _shootAtPlayer;
    private Weapon _weapon;
    private float _loopWidth;

    protected override void Setup()
    {
        base.Setup();

        if (_maxHeight < _minHeight)
        {
            Debug.LogError($"The maximum height cannot be smaller than the minimum height\tmax:{_maxHeight} min:{_minHeight}");
        }

        float spriteWidth = _sprite.bounds.size.x;
        _loopWidth = Utils.GetHorizontalScreenBounds() - spriteWidth / 2;
        _weapon = new Weapon(3, 1);
        _shootAtPlayer = false;
        //_shootAtPlayer = Random.value < 0.5f;
        _shootTimer = 0;

        StartCoroutine(MainCoroutine());
    }

    private IEnumerator MainCoroutine()
    {
        yield return StartCoroutine(ArrivalCoroutine());

        while (true)
        {
            NormalBehavior();

            yield return null;
        }
    }

    private void NormalBehavior()
    {
        _shootTimer -= Time.deltaTime;

        if(_shootTimer <= 0)
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


    private IEnumerator ArrivalCoroutine()
    {
        // Get the current position
        Vector3 startPos = transform.position;

        // Randomly choose an arrival target
        float randomY = Random.Range(_minHeight, _maxHeight);
        float randomX = Random.Range(-_loopWidth, _loopWidth);
        Vector3 target = new(randomX, randomY);

        float time = 0;
        while (time < _arrivalTimeSeconds)
        {
            // Lerp towards the target
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, target, time / _arrivalTimeSeconds);
            yield return null;
        }
    }
}
