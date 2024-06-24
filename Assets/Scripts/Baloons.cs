using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloons : MonoBehaviour
{
    private int _baloon;

    public int Baloon
    {
        get
        {
            return _baloon;
        }
        set
        {

            if (value > 0)
            {
                _baloon = value;
            }
            else
            {
                _baloon = 0;
                Debug.Log("Baloon Finished");
            }
        }
    }

    public void ResetBaloon()
    {
        Baloon = 0;
    }
}
