using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] public GameObject[] tiles;
    [SerializeField] public GameObject duck;
    [SerializeField] public GameObject rocket;
    [SerializeField] public GameObject baloon;
    [SerializeField] int numberOfBaloons = 5;
    [SerializeField] int numberOfDucks = 5;    
    [SerializeField] public int totalMoves = 30;
    [SerializeField] public int totalBaloons = 10;

    [SerializeField] public int coordX = 6;
    [SerializeField] public int coordY = 6;

    public Tile[,] tileMatrix;

    Moves remainingMoves;
    Baloons remaininBaloons;



    private void Awake()
    {
        remainingMoves = FindObjectOfType<Moves>();
        remaininBaloons = FindObjectOfType<Baloons>();
        tileMatrix = new Tile[coordX, coordY];
    }

    private void Start()
    {
        GridCreator();

        for (int i = 1; i <= numberOfDucks; i++)
        {
            SetRandomDuckCoordinate();
        }

        for(int j = 1; j <= numberOfBaloons; j++)
        {
            SetRandomBaloonCoordinate();
        }

        remainingMoves.Move = totalMoves;
        remaininBaloons.Baloon = totalBaloons;
    }


    public void GridCreator()
    {
        for (int x = 0; x < coordX; x++)
        {
            for (int y = 0; y < coordY; y++)
            {
                RandomTileGenerator(new Vector2Int(x, y));
            }
        }
    }

    public void RandomTileGenerator(Vector2Int coordinate)
    {
        var randomNumberGenetaror = Random.Range(0, tiles.Length);

        var generatedTile = Instantiate(tiles[randomNumberGenetaror], new Vector3Int(coordinate.x, coordinate.y, 0), Quaternion.identity);

        generatedTile.transform.parent = this.transform;

        tileMatrix[coordinate.x, coordinate.y] = new Tile { Object = generatedTile, Coordinate = coordinate, TileColor = randomNumberGenetaror, DistinctiveType = DistinctiveType.Tile };

    }

    public void TryApplyGravity()
    {
        for (int x = 0; x < coordX; x++)
        {
            int counter = 0;

            for (int y = 0; y < coordY; y++)
            {
                if (tileMatrix[x, y] == null)
                {
                    counter++;
                }
                else if (counter > 0)
                {
                    var tile = tileMatrix[x, y];
                    tileMatrix[x, y] = null;
                    tileMatrix[x, y - counter] = tile;

                    tile.Coordinate = new Vector2Int(x, y - counter);
                    tile.Object.transform.position = new Vector3Int(x, y - counter, 0);
                }
            }
        }
    }

    public void DuckCreator(Vector2Int coordinate)
    {
        var duckk = Instantiate(this.duck, (Vector3Int)coordinate, Quaternion.identity);
        tileMatrix[coordinate.x, coordinate.y] = new Tile { Object = duckk, Coordinate = coordinate, DistinctiveType = DistinctiveType.Duck };
    }

    public void SetRandomDuckCoordinate()
    {
        var randomX = Random.Range(1, coordX);
        var randomY = Random.Range(1, coordY);
        var randomCoordinate = new Vector2Int(randomX, randomY);

        var randomCoordinateTile = tileMatrix[randomX, randomY];
        Destroy(randomCoordinateTile.Object);
        randomCoordinateTile = null;

        DuckCreator(randomCoordinate);
    }

    public void RocketCreator(Vector2Int coordinate)
    {
        var rockett = Instantiate(this.rocket, (Vector3Int)coordinate, Quaternion.identity);
        tileMatrix[coordinate.x, coordinate.y] = new Tile { Object = rockett, Coordinate = coordinate, DistinctiveType = DistinctiveType.Rocket };
    }

    public void SetRocketCoordinate(Tile rocket)
    {
        RocketCreator(rocket.Coordinate);
    }

    public void BaloonCreator(Vector2Int coordinate)
    {
        var baloon = Instantiate(this.baloon, (Vector3Int)coordinate, Quaternion.identity);
        tileMatrix[coordinate.x, coordinate.y] = new Tile { Object = baloon, Coordinate = coordinate, DistinctiveType = DistinctiveType.Baloon };
    }

    public void SetRandomBaloonCoordinate()
    {
        var randomX = Random.Range(1, coordX);
        var randomY = Random.Range(1, coordY);
        var randomCoordinate = new Vector2Int(randomX, randomY);

        var randomCoordinateTile = tileMatrix[randomX, randomY];
        Destroy(randomCoordinateTile.Object);
        randomCoordinateTile = null;

        BaloonCreator(randomCoordinate);
    }

    public void TryFillEmptySpace()
    {
        for (int i = 0; i < coordX; i++)
        {
            for (int j = 0; j < coordY; j++)
            {
                var tile = tileMatrix[i, j];

                if (tile == null)
                {
                    RandomTileGenerator(new Vector2Int(i, j));
                }
            }
        }
    }

    public void CheckBottomRow()
    {
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < coordY; j++)
            {
                var duck = tileMatrix[j, i];
                if (duck.DistinctiveType == DistinctiveType.Duck)
                {
                    Destroy(duck.Object);
                    tileMatrix[j, i] = null;
                    TryApplyGravity();
                    TryFillEmptySpace();
                }
            }
        }
    }

    public void ReduceNumberOfMoves()
    {
        remainingMoves.Move--;        
    }

    public void ReduceNumberOfBaloons()
    {
        remaininBaloons.Baloon--;
    }
    




}
