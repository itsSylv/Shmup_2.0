using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;

    private void Awake()
    {
        Setup();
    }
    
    protected virtual void Setup()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
