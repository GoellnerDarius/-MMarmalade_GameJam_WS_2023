using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private int _spawnRate;
    [SerializeField] private GameObject _colliders;
    [SerializeField] private SpriteRenderer _colliderPrefab;

    [SerializeField] private ColliderData[] _colliderData;

    private Vector2 _itemSize;
    private int _itemsPerRow;
    private int _itemsPerCol;
    private float _spawnCounter;

    private void Start()
    {
        // _colSize = (int)(Mathf.Abs(_endPoint.y - _startPoint.y) / _itemsPerRow);
        // _itemSize = _colliderPrefab.sprite.textureRect.size;
        _itemSize = _colliderPrefab.bounds.size;
        // No Math.Abs to get the "direction" for the case when end - start is negative (due to vector position)
        float gridSizeX = _endPoint.position.x - _startPoint.position.x;
        float gridSizeY = _endPoint.position.y - _startPoint.position.y;
        _itemsPerRow = (int)(gridSizeX / _itemSize.x);
        _itemsPerCol = (int)(gridSizeY / _itemSize.y);
    }

    private void Update()
    {
        _spawnCounter += Time.deltaTime;

        if (_spawnCounter >= _spawnRate)
        {
            _spawnCounter = 0;
            SpawnCollider();
        }
    }

    private void SpawnCollider()
    {
        float y = _startPoint.position.y + UnityEngine.Random.Range(0, _itemsPerCol) * _itemSize.y;
        float x = _startPoint.position.x + UnityEngine.Random.Range(0, _itemsPerRow) * _itemSize.x;

        int index = UnityEngine.Random.Range(0, _colliderData.Length);

        var newCollider = Instantiate(_colliderData[index].Prefab, new Vector2(x, y), Quaternion.identity, _colliders.transform);
    }

    [Serializable]
    public struct ColliderData
    {
        [Range(0, 1)] public float SpawnChange;
        public GameObject Prefab;
    }
}
