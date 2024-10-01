using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWrapper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float _loopWidth;

    private void Awake()
    {
        float screenWidth = (Camera.main.orthographicSize * 2) * Camera.main.aspect;
        float spriteWidth = _spriteRenderer.bounds.size.x;
        _loopWidth = (screenWidth + spriteWidth) / 2;
    }

    private void Update()
    {
        float xPos = transform.position.x;
        if (Pos(xPos) >= _loopWidth)
        {
            float sign = Mathf.Sign(-transform.position.x);
            Vector3 newPos = new Vector3(_loopWidth * sign, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }

    /// <summary>
    /// Return the given number as a positive number
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private float Pos(float x)
    {
        return Mathf.Sqrt(x * x);
    }
}
