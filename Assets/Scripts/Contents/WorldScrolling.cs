using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    private Vector2Int _currentTilePosition = new Vector2Int(0,0);
    [SerializeField ]
    private Vector2Int _playerTilePosition;
    private Vector2Int _onTileGridPlayerPosition;

    [SerializeField]
    private float _tileSize = 25f;
    private GameObject[,] _terrainTiles;


    [SerializeField] 
    private int terrainTileHorizontalCount;
    [SerializeField]
    private int terrainTileVerticalCount;

    [SerializeField]
    private int fieldOfVisionHeight = 3;
    [SerializeField]
    private int fieldOfVisionWidth = 3;

    private void Awake()
    {
        _playerTransform = Managers.Game.getPlayer().transform;
        _terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update()
    {
        _playerTilePosition.x = (int)(_playerTransform.position.x / _tileSize);
        _playerTilePosition.y = (int)(_playerTransform.position.y / _tileSize);

        _playerTilePosition.x -= _playerTransform.position.x < 0 ? 1 : 0;
        _playerTilePosition.y -= _playerTransform.position.y < 0 ? 1 : 0;

        if (_currentTilePosition != _playerTilePosition)
        {
            _currentTilePosition = _playerTilePosition;

            _currentTilePosition.x = CalculatePositionOnAxis(_currentTilePosition.x, true);
            _currentTilePosition.y = CalculatePositionOnAxis(_currentTilePosition.y, false);
            UpdateTileInScreen();
        }
    }

    private void UpdateTileInScreen()
    {
        for(int pov_x= -(fieldOfVisionWidth/2); pov_x <= fieldOfVisionWidth/2; pov_x++)
        {
            for (int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionWidth/2; pov_y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(_playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(_playerTilePosition.y + pov_y, false);

                GameObject tile = _terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(
                    _playerTilePosition.x + pov_x,
                    _playerTilePosition.y + pov_y
                    );
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * _tileSize, y * _tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if(currentValue >= 0)
                currentValue = currentValue % terrainTileHorizontalCount;
            else
            {
                currentValue += 1;
                currentValue = (terrainTileHorizontalCount - 1) + currentValue % terrainTileHorizontalCount;
            }
                
        }
        else
        {
            if (currentValue >= 0)
                currentValue = currentValue % terrainTileVerticalCount;
            else
            {
                currentValue += 1;
                currentValue = (terrainTileVerticalCount - 1) + currentValue % terrainTileVerticalCount;
            }
                
        }

        return (int)currentValue;
    }

    internal void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        _terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}
