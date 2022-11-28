using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum GAMESTATE
{
    START,
    RUN,
    LEVEL_END,
    END
}

public class GameController : MonoBehaviour
{
    static public GameController instance;

    public GameObject player;
    public GameObject crate;
    public GameObject level_container;
    public GameObject[] levels;

    public int curr_level = 0;


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
            case GAMESTATE.LEVEL_END:
                Time.timeScale = 0.0f;
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

    public void StartGame()
    {
        NextLevel();
    }

    public void NextLevel()
    {

        if (curr_level >= levels.Length)
        {
            setGameState(GAMESTATE.END);
        }
        else
        {
            curr_level++;
            foreach (Transform t in level_container.transform) Destroy(t.gameObject);
            Instantiate(levels[curr_level - 1], level_container.transform);
            CountdownTimer.instance.ResetTimer();
            player.GetComponent<PlayerController>().Reset();
            crate.GetComponent<CrateController>().Reset();
            setGameState(GAMESTATE.RUN);
        }
    }

    public void ResetGame() 
    {
        CountdownTimer.instance.ResetTimer();

        player.GetComponent<PlayerController>().Reset();
        crate.GetComponent<CrateController>().Reset();

        // Won't be necessary anymore with the level setup
        ResetSmashers();
        ResetCells();

        setGameState(GAMESTATE.START);
    }

    public void ResetSmashers()
    {
        GameObject[] smashers = GameObject.FindGameObjectsWithTag("smasher");
        foreach (GameObject smasher in smashers) smasher.GetComponent<SmasherController>().Reset();
    }

    public void ResetCells()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("GridCell");
        foreach (GameObject cell in cells) cell.GetComponent<GridCell>().Reset();
    }
}
