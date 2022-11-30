using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    GameController gameControllerScript;

    public GameObject endScreen;
    public GameObject startScreen;
    public GameObject levelScreen;
    public GameObject tutorialScreen;

    void Start()
    {
        gameControllerScript = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameControllerScript.getGameState())
        {
            case GAMESTATE.START:
                levelScreen.SetActive(false);
                startScreen.SetActive(true);
                endScreen.SetActive(false);
                tutorialScreen.SetActive(false);
                break;
            case GAMESTATE.LEVEL_START:
                levelScreen.SetActive(false);
                startScreen.SetActive(false);
                endScreen.SetActive(false);
                tutorialScreen.SetActive(true);
                break;
            case GAMESTATE.LEVEL_END:
                levelScreen.SetActive(true);
                startScreen.SetActive(false);
                endScreen.SetActive(false);
                tutorialScreen.SetActive(false);
                break;
            case GAMESTATE.END:
                levelScreen.SetActive(false);
                startScreen.SetActive(false);
                endScreen.SetActive(true);
                tutorialScreen.SetActive(false);
                break;
            case GAMESTATE.RUN:
                levelScreen.SetActive(false);
                startScreen.SetActive(false);
                endScreen.SetActive(false);
                tutorialScreen.SetActive(false);
                break;
            default:
                break;
        }
    }
}
