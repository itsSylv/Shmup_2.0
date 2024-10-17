using Project.Utils;
using System.Collections;
using UnityEngine;

public class MovingEnemy : DamageableEnemy
{
    private enum MoveDirection
    {
        Left,
        Right
    }

    [Header("Movement")]
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _arrivalTimeSeconds;
    [SerializeField] private SpriteRenderer _sprite;

    private MoveDirection _direction;
    private bool? _moveTowardsPlayer = null;
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

        StartCoroutine(ArrivalCoroutine());
    }

    private void Update()
    {
        if (_moveTowardsPlayer == null)
        {
            return;
        }

        HandleMovement();

        // I really really dont feel like making a whole new variable to check whether the enemy is too low
        // Have a hardcoded value.
        if (_moveTowardsPlayer == true && transform.position.y > -3.5)
        {
            transform.position -= new Vector3(0, (_movementSpeed / 4) * Time.deltaTime);
        }
    }

    private void HandleMovement()
    {
        Vector3 movement = new(_movementSpeed * Time.deltaTime, 0);

        switch (_direction)
        {
            case MoveDirection.Left:
                transform.position -= movement;
                break;
            case MoveDirection.Right:
                transform.position += movement;
                break;
            default:
                Debug.LogError("Move direction is neither left or right");
                break;
        }

        if (Utils.Pos(transform.position.x) > _loopWidth)
        {
            _direction = transform.position.x > 0 ? MoveDirection.Left : MoveDirection.Right;
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

        _moveTowardsPlayer = Random.value < 0.5f;
        _direction = (MoveDirection)Random.Range(0, 2);
    }
}
