using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private List<GameObject> _inactiveObjects;
    [SerializeField] private int _islandAmount;
    [SerializeField] private float _despawnPoint;
    private List<GameObject> _activeObjects;

    private void Awake()
    {
        _activeObjects = new List<GameObject>();
        
        for (int i = 0; i < _islandAmount; i++)
        {
            Vector3 targetPosition = new Vector3(Random.Range(-1f, 1f), 0 + 7 * i, 0);
            MakeObject(targetPosition);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _activeObjects.Count; i++)
        {
            _activeObjects[i].transform.position += Vector3.down * (_scrollSpeed * Time.deltaTime);

            if (_activeObjects[i].transform.position.y <= _despawnPoint)
            {
                Vector3 targetPosition = new Vector3(Random.Range(-1f, 1f),
                    _activeObjects[_activeObjects.Count - 1].transform.position.y + 7, 0);
                MakeObject(targetPosition);
                ReturnObject(_activeObjects[i]);
            }
        }
    }
    
    private void MakeObject(Vector3 position)
    {
        GameObject island = GetInactiveObject();
        _inactiveObjects.Remove(island);
        _activeObjects.Add(island);
        island.transform.position = position;
        island.SetActive(true);
    }

    private GameObject GetInactiveObject()
    {
        int randomIndex = Random.Range(0, _inactiveObjects.Count);
        return _inactiveObjects[randomIndex];
    }

    private void ReturnObject(GameObject obj)
    {
        _activeObjects.Remove(obj);
        _inactiveObjects.Add(obj);
        obj.SetActive(false);
    }
}
