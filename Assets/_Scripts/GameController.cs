using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum GAMESTATE
{
    START,
    RUN,
    END
}

public class GameController : MonoBehaviour
{
    static public GameController instance;

    public GameObject player;
    public GameObject crate;

    private GAMESTATE gameState;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ResetGame();
        gameState = GAMESTATE.START;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GAMESTATE.START:
                Time.timeScale = 0.0f;
                break;
            case GAMESTATE.RUN:
                Time.timeScale = 1.0f;
                break;
            case GAMESTATE.END:
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }
    }

    public GAMESTATE getGameState()
    {
        return gameState;
    }

    public void setGameState(GAMESTATE state)
    {
        gameState = state;
    }

    public void ResetGame() 
    {
        CountdownTimer.instance.ResetTimer();

        player.GetComponent<PlayerController>().Reset();
        crate.GetComponent<CrateController>().Reset();

        GameObject[] smashers = GameObject.FindGameObjectsWithTag("smasher");
        foreach (GameObject smasher in smashers) smasher.GetComponent<SmasherController>().Reset();

        GameObject[] cells = GameObject.FindGameObjectsWithTag("GridCell");
        foreach (GameObject cell in cells) cell.GetComponent<GridCell>().Reset();

        setGameState(GAMESTATE.START);
    }
}
