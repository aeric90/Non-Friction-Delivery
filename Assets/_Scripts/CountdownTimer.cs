using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    static public CountdownTimer instance;

    public TMPro.TextMeshProUGUI timerText;
    public float gameTime = 300;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timerText.transform.localPosition = new Vector3((0 - (Screen.width/2)) + 120, (0 + (Screen.height / 2)) - 40, 0);
        time = gameTime;
        timerText.text = "Time Left:";
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            timerText.text = "Time Left: " + minutes + ":" + seconds;
        }
        else
        {
            timerText.text = "Time is out!";
            GameController.instance.setGameState(GAMESTATE.END);
        }
    }

    public void ResetTimer()
    {
        time = gameTime;
        timerText.text = "Time Left:";
    }
}
