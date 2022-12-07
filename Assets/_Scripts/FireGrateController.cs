using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FIREGRATESTATE
{
    DELAY,
    BURN,
    PAUSE
}

public class FireGrateController : MonoBehaviour
{
    public float delayTime = 0.0f;
    public float burnTime = 0.0f;
    public float pauseTime = 0.0f;
    public bool alwaysOn = false;

    public FIREGRATESTATE fireGrateState = FIREGRATESTATE.DELAY;
    public ParticleSystem flames;
    private BoxCollider flameBox;
    private AudioSource flameSource;
    private float stageTime;

    // Start is called before the first frame update
    void Start()
    {
        flameBox = GetComponent<BoxCollider>();
        flameSource = GetComponent<AudioSource>();
        if (flames.isPlaying) flames.Stop();
        if (flameSource.isPlaying) flameSource.Stop();
        flameSource.mute = true;
        stageTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (fireGrateState)
        {
            case FIREGRATESTATE.DELAY:
                flameBox.enabled = false;
                if (flames.isPlaying) flames.Stop();
                if (flameSource.isPlaying) flameSource.Stop();
                if (Time.time - stageTime >= delayTime) switchState(FIREGRATESTATE.BURN);
                break;
            case FIREGRATESTATE.BURN:
                flameBox.enabled = true;
                if(!flames.isPlaying) flames.Play();
                flameSource.mute = false;
                if (!flameSource.isPlaying) flameSource.Play();
                if (Time.time - stageTime >= burnTime && !alwaysOn) switchState(FIREGRATESTATE.PAUSE);
                break;
            case FIREGRATESTATE.PAUSE:
                flameBox.enabled = false;
                if (flames.isPlaying) flames.Stop();
                if (flameSource.isPlaying) flameSource.Stop();
                if (Time.time - stageTime >= pauseTime)
                {
                    flameSource.pitch = Random.Range(0.8f, 1.2f);
                    switchState(FIREGRATESTATE.BURN);
                }
                break;
            default:
                break;
        }
    }

    private void switchState(FIREGRATESTATE state)
    {
        fireGrateState = state;
        stageTime = Time.time;
    }
}
