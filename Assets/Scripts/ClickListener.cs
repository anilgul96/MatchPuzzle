using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{    
    GridHandler gridHandler;

    List<Vector2Int> Directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };

    List<Tile> SameColorTileList = new List<Tile>();

    List<Vector2Int> CheckedDirectionsList = new List<Vector2Int>();

    private void Awake()
    {
        gridHandler = FindObjectOfType<GridHandler>();
        
    }

    private void OnMouseDown()
    {
        var selectedTile = gameObject;

        var selectedTileCoordinate = new Vector2Int((int)selectedTile.transform.position.x, (int)selectedTile.transform.position.y);

        TryDetectSameColorTile(selectedTileCoordinate);

        TryDestoryTileChain(selectedTileCoordinate);


    }

    public void TryDetectSameColorTile(Vector2Int coordinate)
    {

        var currentTile = gridHandler.tileMatrix[coordinate.x, coordinate.y];

        var currentTileCoordinate = currentTile.Coordinate;

        var currentTileColor = currentTile.TileColor;

        foreach (var direction in Directions)
        {
            var nextCoordinate = currentTileCoordinate + direction;

            if (nextCoordinate.x < 0 || nextCoordinate.y < 0 || nextCoordinate.x > gridHandler.coordX - 1 || nextCoordinate.y > gridHandler.coordY - 1) continue;

            var nextCoordinateTile = gridHandler.tileMatrix[nextCoordinate.x, nextCoordinate.y];

            if (CheckedDirectionsList.Contains(nextCoordinate) || nextCoordinateTile == null) continue;

            CheckedDirectionsList.Add(nextCoordinate);

            var nextTileColor = nextCoordinateTile.TileColor;

            var nextTilesType = nextCoordinateTile.DistinctiveType;

            if (currentTileColor == nextTileColor && nextTilesType == DistinctiveType.Tile)
            {
                SameColorTileList.Add(nextCoordinateTile);
                TryDetectSameColorTile(nextCoordinate);
            }
        }

    }

    public void TryDestoryTileChain(Vector2Int coordinate)
    {
        var selectedTile = gridHandler.tileMatrix[coordinate.x, coordinate.y];

        if (selectedTile.DistinctiveType == DistinctiveType.Rocket)
        {
            TryDestroyRow(coordinate.y);
        }
        else
        {
            if (SameColorTileList.Count >= 5)
            {
                foreach (var tile in SameColorTileList)
                {
                    Destroy(tile.Object);
                    gridHandler.tileMatrix[tile.Coordinate.x, tile.Coordinate.y] = null;
                }
                gridHandler.SetRocketCoordinate(selectedTile);

            }
            else if (SameColorTileList.Count >= 2)
            {
                foreach (var tile in SameColorTileList)
                {
                    Destroy(tile.Object);
                    gridHandler.tileMatrix[tile.Coordinate.x, tile.Coordinate.y] = null;
                }
            }
        }

        SameColorTileList.Clear();
        CheckedDirectionsList.Clear();
        gridHandler.TryApplyGravity();
        gridHandler.TryFillEmptySpace();
        gridHandler.CheckBottomRow();
        gridHandler.ReduceNumberOfMoves();

    }
    
    public void TryDestroyRow(int coordY)
    {
        for (int x = 0; x < gridHandler.coordX; x++)
        {
            var tile = gridHandler.tileMatrix[x, coordY];

            if (tile != null && tile.DistinctiveType != DistinctiveType.Duck)
            {
                Destroy(tile.Object);
                gridHandler.tileMatrix[x, coordY] = null;
            }
        }
    }

    













}


