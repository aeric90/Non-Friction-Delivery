using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SMAHSERSTATE
{
    DELAY,
    SMASH,
    LIFT,
    PAUSE
}

public class SmasherController : MonoBehaviour
{
    public float delayTime = 0.0f;
    public float liftSpeed = 20.0f;
    public float pauseTime = 0.0f;

    public SMAHSERSTATE smasherState = SMAHSERSTATE.DELAY;
    private Vector3 startPos;
    public float stageTime;
    private bool smashPlayed = false;

    private AudioSource smasherSource;

    private void Start()
    {
        startPos = transform.position;
        stageTime = Time.time;
        smasherSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        switch (smasherState)
        {
            case SMAHSERSTATE.DELAY:
                if (Time.time - stageTime >= delayTime) switchState(SMAHSERSTATE.SMASH);
                break;
            case SMAHSERSTATE.SMASH:
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -2000.0f, 0.0f));
                if (Time.time - stageTime >= 2.0f) switchState(SMAHSERSTATE.LIFT);
                break;
            case SMAHSERSTATE.LIFT:
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * liftSpeed);
                if (transform.position.y >= startPos.y) switchState(SMAHSERSTATE.PAUSE);
                break;
            case SMAHSERSTATE.PAUSE:
                if (Time.time - stageTime >= pauseTime)
                {
                    smasherSource.pitch = Random.Range(0.8f, 1.2f);
                    switchState(SMAHSERSTATE.SMASH);
                    smashPlayed = false;
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "floor" && !smashPlayed)
        {
            smasherSource.Play();
            smashPlayed = true;
        }    
    }

    private void switchState(SMAHSERSTATE state)
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        smasherState = state;
        stageTime = Time.time;
    }

    public void Reset()
    {
        this.transform.position = startPos;
        switchState(SMAHSERSTATE.DELAY);
    }
}
