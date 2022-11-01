using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    GameController gameControllerScript;
    GAMESTATE gState;
    public GameObject endScreen;
    public GameObject startScreen;
    void Start()
    {
        gameControllerScript = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

        gState = gameControllerScript.getGameState();
        Debug.Log(gState);
        if(gState == GAMESTATE.END)
        {
            endScreen.SetActive(true);
        }

        if (gState == GAMESTATE.RUN)
        {
            startScreen.SetActive(false);
            playGame();
        }
    }

    public void playGame()
    {
        gameControllerScript.setGameState(GAMESTATE.RUN);
    }
}
