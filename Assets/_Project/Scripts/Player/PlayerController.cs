using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private void Update()
    {
        transform.position += InputManager.Main.MoveDirection * (Time.deltaTime * _movementSpeed);
    }
}
