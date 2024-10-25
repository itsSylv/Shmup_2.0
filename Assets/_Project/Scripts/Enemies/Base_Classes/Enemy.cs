using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Utils;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _arrivalTimeSeconds;
    [SerializeField] private SpriteRenderer _sprite;

    protected Rigidbody2D rb;
    protected float LoopWidth;

    private void Awake()
    {
        Setup();
    }
    
    protected virtual void Setup()
    {
        if (_maxHeight < _minHeight)
        {
            Debug.LogError($"The maximum height cannot be smaller than the minimum height\tmax:{_maxHeight} min:{_minHeight}");
        }

        rb = GetComponent<Rigidbody2D>();

        float spriteWidth = _sprite.bounds.size.x;
        LoopWidth = Utils.GetHorizontalScreenBounds() - spriteWidth / 2;

        StartCoroutine(MainCoroutine());
    }

    private IEnumerator MainCoroutine()
    {
        yield return StartCoroutine(ArrivalCoroutine());

        while (true)
        {
            Behavior();

            yield return null;
        }
    }
    private IEnumerator ArrivalCoroutine()
    {
        // Get the current position
        Vector3 startPos = transform.position;

        // Randomly choose an arrival target
        float randomY = Random.Range(_minHeight, _maxHeight);
        float randomX = Random.Range(-LoopWidth, LoopWidth);
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

    /// <summary>
    /// Runs every frame after the enemy has arrived to the scene
    /// </summary>
    protected abstract void Behavior();
}
