using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] Images;
    GameObject GameBoardObject;

    int tokenValue = -1;

    public int GetTokenValue()
        => tokenValue;

    private void OnMouseDown()
    {
        if (tokenValue == -1 && GameBoardObject.GetComponent<GameBoardScript>().GetWinnerTokenValue() == -1)
        {
            int currentTurnIndex = GameBoardObject.GetComponent<GameBoardScript>().GetTurnIndex();
            tokenValue = currentTurnIndex;
            spriteRenderer.sprite = Images[tokenValue];

            GameBoardObject.GetComponent<GameBoardScript>().UpdateFlags();
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameBoardObject = GameObject.Find("GameBoard");
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
