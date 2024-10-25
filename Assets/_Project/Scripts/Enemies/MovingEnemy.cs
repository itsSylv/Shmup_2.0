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
    [SerializeField] private float _movementSpeed;

    private bool _moveTowardsPlayer;
    private MoveDirection _direction;

    protected override void Setup()
    {
        base.Setup();

        _moveTowardsPlayer = Random.value < 0.5f;
    }

    protected override void Behavior()
    {
        HandleMovement();

        if (_moveTowardsPlayer && transform.position.y > Player.transform.position.y)
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

        if (Mathf.Abs(transform.position.x) > LoopWidth)
        {
            _direction = transform.position.x > 0 ? MoveDirection.Left : MoveDirection.Right;
        }
    }
}
