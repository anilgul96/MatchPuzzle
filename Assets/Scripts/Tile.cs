using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2Int Coordinate { get; set; }

    public GameObject Object { get; set; }

    public int TileColor { get; set; }

    public DistinctiveType DistinctiveType { get; set; }


}

public enum DistinctiveType
{
    Tile,
    Duck,
    Rocket,
    Baloon,

}


