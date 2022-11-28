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
                startScreen.SetActive(true);
                endScreen.SetActive(false);
                break;
            case GAMESTATE.LEVEL_END:
                levelScreen.SetActive(true);
                break;
            case GAMESTATE.END:
                endScreen.SetActive(true);
                break;
            case GAMESTATE.RUN:
                levelScreen.SetActive(false);
                startScreen.SetActive(false);
                endScreen.SetActive(false);
                break;
            default:
                break;
        }
    }
}
