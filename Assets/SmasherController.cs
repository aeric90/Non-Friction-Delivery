using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SMAHSERSTATE
{
    DELAY,
    SMASH,
    LIFT,
    PAUSE
}

public class SmasherController : MonoBehaviour
{
    public float delayTime = 0.0f;
    public float liftTime = 0.0f;
    public float pauseTime = 0.0f;

    private SMAHSERSTATE smasherState = SMAHSERSTATE.DELAY;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (smasherState)
        {
            case SMAHSERSTATE.DELAY:

                break;
            case SMAHSERSTATE.SMASH:
                break;
            case SMAHSERSTATE.LIFT:
                break;
            case SMAHSERSTATE.PAUSE:
                break;
            default:
                break;
        }
        
    }
}
