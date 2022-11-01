using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText;
    float time = 300;
    GameController gameControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = GameObject.Find("Game Controller").GetComponent<GameController>();
        timerText.transform.localPosition = new Vector3((0 - (Screen.width/2)) + 120, (0 + (Screen.height / 2)) - 40, 0);
        time = 300;
        timerText.text = "Time Left: 5:00";
    }

    void UpdateTimer()
    {
        if (time > 0 && gameControllerScript.getGameState() == GAMESTATE.RUN)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            timerText.text = "Time Left: " + minutes + ":" + seconds;
        }
        else
        {
            timerText.text = "Time is out!";
            gameControllerScript.setGameState(GAMESTATE.END);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }
}
