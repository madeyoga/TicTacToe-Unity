using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardScript : MonoBehaviour
{
    private static GameObject WhoWinsText, WhoToMovesText;
    public GameObject Token1;
    public GameObject Token2;
    public GameObject Token3;
    public GameObject Token4;
    public GameObject Token5;
    public GameObject Token6;
    public GameObject Token7;
    public GameObject Token8;
    public GameObject Token9;
    private GameObject[,] boards;

    // FLAGS
    private int currentTurn = -1;
    private int winnerTokenValue = -1;
    private bool gameFinished = false;
    private bool boardFull = false;

    public int GetTurnIndex()
    {
        currentTurn += 1;
        return currentTurn % 2;
    }

    public int GetWinnerTokenValue()
    {
        return winnerTokenValue;
    }

    public void UpdateFlags()
    {
        Debug.Log("UpdateFlags");
        if (CheckWinning(0))
        {
            winnerTokenValue = 0;
            gameFinished = true;
        }
        else if (CheckWinning(1))
        {
            winnerTokenValue = 1;
            gameFinished = true;
        }
        else
        {
            boardFull = BoardFull();
            if (boardFull)
            {
                winnerTokenValue = -1;
                gameFinished = true;
            }
        }



        // check game
        if (gameFinished)
        {
            // DRAW
            if (boardFull && winnerTokenValue == -1)
            {
                WhoWinsText.GetComponent<Text>().text = "DRAW";
            }
            else
            {
                string stat = "DRAW";
                if (winnerTokenValue == 0)
                {
                    stat = "Player X WIN";
                }
                else if (winnerTokenValue == 1)
                {
                    stat = "Player O WIN";
                }
                WhoWinsText.GetComponent<Text>().text = stat;
                WhoWinsText.gameObject.SetActive(true);
            }
        }

        if (currentTurn % 2 == 0)
        {
            WhoToMovesText.GetComponent<Text>().text = "O to move";
        }
        else
        {
            WhoToMovesText.GetComponent<Text>().text = "X to move";
        }
    }

    private bool BoardFull()
    {
        for (int i = 0; i < boards.GetLength(0); i++)
        {
            for (int j = 0; j < boards.GetLength(1); j++)
            {
                int tokenValue = boards[i, j].GetComponent<TokenScript>().GetTokenValue();
                if (tokenValue < 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool CheckWinning(int tokenValue)
    {
        int tokenCount;

        // Check Horizontal
        for (int i = 0; i < boards.GetLength(0); i++)
        {
            tokenCount = 0;
            for (int j = 0; j < boards.GetLength(1); j++)
            {
                int currentCursorValue = boards[i, j].GetComponent<TokenScript>().GetTokenValue();
                if (currentCursorValue != tokenValue)
                {
                    break;
                }
                tokenCount += 1;
            }
            if (tokenCount == 3)
            {
                return true;
            }
        }

        // Check Vertical
        for (int i = 0; i < boards.GetLength(0); i++)
        {
            tokenCount = 0;
            for (int j = 0; j < boards.GetLength(1); j++)
            {
                int currentCursorValue = boards[j, i].GetComponent<TokenScript>().GetTokenValue();
                if (currentCursorValue != tokenValue)
                {
                    break;
                }
                tokenCount += 1;
            }
            if (tokenCount == 3)
            {
                return true;
            }
        }

        // First Diagonal
        tokenCount = 0;
        for (int i = 0; i < boards.GetLength(0); i++)
        {
            int currentCursorValue = boards[i, i].GetComponent<TokenScript>().GetTokenValue();
            if (currentCursorValue != tokenValue)
            {
                break;
            }
            tokenCount += 1;
        }
        if (tokenCount == 3)
        {
            return true;
        }

        // Second Diagonal
        tokenCount = 0;
        int _j = boards.GetLength(1) - 1;
        for (int i = 0; i < boards.GetLength(0); i++)
        {
            int currentCursorValue = boards[i, _j].GetComponent<TokenScript>().GetTokenValue();
            if (currentCursorValue != tokenValue)
            {
                break;
            }
            tokenCount += 1;

            _j -= 1;
        }
        if (tokenCount == 3)
        {
            return true;
        }
        return false;
    }

    private void Awake()
    {
        boards = new GameObject[,]
        {
            { Token1, Token2, Token3 },
            { Token4, Token5, Token6 },
            { Token7, Token8, Token9 }
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        WhoWinsText = GameObject.Find("WhoWins");
        WhoToMovesText = GameObject.Find("WhoToMove");

        WhoWinsText.SetActive(false);
        WhoToMovesText.SetActive(true);

        WhoToMovesText.GetComponent<Text>().text = "X to move";
    }

    private void FixedUpdate()
    {
    }
}
