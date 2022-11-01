using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

enum GAMESTATE
{
    START,
    RUN,
    END
}

public class GameController : MonoBehaviour
{

    private GAMESTATE gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GAMESTATE.START;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GAMESTATE.START:
                break;
            case GAMESTATE.RUN:
                break;
            case GAMESTATE.END:
                break;
            default:
                break;
        }
    }
}
