using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    private int _moves;

    public int Move
    {
        get
        {
            return _moves;
        }
        set
        {

            if (value > 0)
            {
                _moves = value;
                Debug.Log($"Remaining Moves: {value}");
            }
            else
            {
                _moves = 0;
                Debug.Log("Game Finished");
            }
        }
    }

    public void ResetMoves()
    {
        Move = 0;
    }
}
