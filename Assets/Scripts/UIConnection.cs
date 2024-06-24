using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIConnection : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI remainingMovesText;
    [SerializeField] TextMeshProUGUI remainingBaloonText;
    Moves remaininMoves;
    Baloons remainingBaloon;

    private void Awake()
    {
        remaininMoves = FindObjectOfType<Moves>();
        remainingBaloon = FindObjectOfType<Baloons>();
    }

    void Update()
    {
        CalculateRemainingMoves();
        CalculateRemainingBaloons();
    }

    public void CalculateRemainingMoves()
    {
        if (remaininMoves.Move > 0)
        {
            remainingMovesText.text = remaininMoves.Move.ToString();
        }
        else
        {
            remainingMovesText.text = "--";
        }
    }

    public void CalculateRemainingBaloons()
    {
        if (remainingBaloon.Baloon > 0)
        {
            remainingBaloonText.text = remainingBaloon.Baloon.ToString();
        }
        else
        {
            remainingBaloonText.text = "--";
        }
    }
}
