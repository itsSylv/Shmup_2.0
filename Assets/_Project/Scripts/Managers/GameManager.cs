using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Main;

    private GameObject _player;
    public GameObject Player => _player;

    private void Awake()
    {
        if (Main != null)
        {
            Destroy(gameObject);
        }

        Main = this;
        
        _player = GameObject.FindGameObjectWithTag("Player");
        
        if (!_player)
        {
            Debug.LogError("Player not found");
        }
    }
}
