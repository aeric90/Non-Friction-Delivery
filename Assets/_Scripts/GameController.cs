using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static Cinemachine.DocumentationSortingAttribute;

public enum GAMESTATE
{
    START,
    RUN,
    LEVEL_END,
    LEVEL_START,
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
        musiccontroller.instance.StopMusic();
        NextLevel();
    }

    public void NextLevel()
    {
        Debug.Log("NEXT LEVEL");
        Time.timeScale = 0.0f;
        curr_level++;
        CubeDebrisController.instance.ClearDebris();
        foreach (Transform t in level_container.transform) Destroy(t.gameObject);
        GameObject levelTemp = Instantiate(levels[curr_level - 1], level_container.transform);
        Time.timeScale = 1.0f;
        player.GetComponent<PlayerController>().Reset();
        crate.GetComponent<CrateController>().Reset();
        Time.timeScale = 0.0f;
        CountdownTimer.instance.ResetTimer();

        if(levelTemp.GetComponent<LevelController>().tutorialText != "")
        {
            setGameState(GAMESTATE.LEVEL_START);
            TutorialScreenController.instance.UpdateTutorialText(levelTemp.GetComponent<LevelController>().tutorialText);
            TutorialScreenController.instance.UpdateLevelText("Level " + curr_level);
        } else
        {
            StartLevel();
        }
    }

    public void ResetGame() 
    {
        curr_level = 0;
        setGameState(GAMESTATE.START);
    }

    public void EndLevel()
    {
        if (curr_level >= levels.Length)
        {
            setGameState(GAMESTATE.END);
        }
        else
        {
            setGameState(GAMESTATE.LEVEL_END);
            NextLevel();
        }
    }

    public void StartLevel()
    {
        setGameState(GAMESTATE.RUN);
    }
}
