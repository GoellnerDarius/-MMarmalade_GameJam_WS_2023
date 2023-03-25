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

    private List<Vector2> _gridPositions = new List<Vector2>();

    private void Start()
    {
        // _colSize = (int)(Mathf.Abs(_endPoint.y - _startPoint.y) / _itemsPerRow);
        // _itemSize = _colliderPrefab.sprite.textureRect.size;
        _itemSize = _colliderPrefab.bounds.size;
        // No Math.Abs to get the "direction" for the case when end - start is negative (due to vector position)
        float gridSizeX = _endPoint.position.x - _startPoint.position.x;
        float gridSizeY = _endPoint.position.y - _startPoint.position.y;
        _itemsPerRow = (int)Math.Abs(gridSizeX / _itemSize.x);
        _itemsPerCol = (int)Math.Abs(gridSizeY / _itemSize.y);
        FillGridPositions();
    }

    private void FillGridPositions()
    {
        for (int row = 0; row <= _itemsPerCol; row++)
        {
            for (int col = 0; col <= _itemsPerRow; col++)
            {
                _gridPositions.Add(new Vector2(col, row));
            }
        }
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
        // float y = _startPoint.position.y + UnityEngine.Random.Range(0, _itemsPerCol) * _itemSize.y;
        // float x = _startPoint.position.x + UnityEngine.Random.Range(0, _itemsPerRow) * _itemSize.x;

        int gridIndex = UnityEngine.Random.Range(0, _gridPositions.Count);
        Vector2 position = _gridPositions[gridIndex];
        _gridPositions.RemoveAt(gridIndex);

        if (_gridPositions.Count == 0)
        {
            FillGridPositions();
        }

        float x = _startPoint.position.x + position.x * _itemSize.x;
        float y = _startPoint.position.y + position.y * _itemSize.y;

        int index = UnityEngine.Random.Range(0, _colliderData.Length);

        if (_colliderData[index].Prefab.GetComponent<SpriteRenderer>().bounds.size.x > 1)
        {
            _gridPositions.Remove(position + new Vector2(1, 0));
            Debug.Log("Position: " + position + ", Position 2: " + (position + new Vector2(1, 0)));
        }

        var newCollider = Instantiate(_colliderData[index].Prefab, new Vector2(x, y), Quaternion.identity, _colliders.transform);
    }

    [Serializable]
    public struct ColliderData
    {
        [Range(0, 1)] public float SpawnChange;
        public GameObject Prefab;
    }
}
