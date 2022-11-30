using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum CRATESTATE_dup
{
    MOVING,
    DEAD
}

public class CrateController_dup : MonoBehaviour
{
    private GameObject crateSpawn;
    private float deathTime;
    private float spawnTime = 2.0f;

    private List<int> checkPoints = new List<int>();

    private CRATESTATE_dup crateState = CRATESTATE_dup.MOVING;

    // Start is called before the first frame update
    void Start()
    {
        crateSpawn = GameObject.Find("Cube Spawn 1");
    }

    // Update is called once per frame
    void Update()
    {
        switch (crateState)
        {
            case CRATESTATE_dup.MOVING:
                // Check collider contact for type of floor
                // Check velocity
                //    Play apporpriate sound effect
                break;
            case CRATESTATE_dup.DEAD:
                // Change to a dead animation
                if (Time.time - deathTime >= spawnTime) Respawn();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroy")
        {
            deathTime = Time.time;
            crateState = CRATESTATE_dup.DEAD;
        }
        if (other.gameObject.tag == "checkpoint")
        {
            if (!checkPoints.Contains(other.gameObject.GetComponent<CheckpointController>().checkPointID))
            {
                checkPoints.Add(other.gameObject.GetComponent<CheckpointController>().checkPointID);
                crateSpawn = other.gameObject.GetComponent<CheckpointController>().cubeSpawn;
            }
        }
        if(other.gameObject.tag == "goal")
        {
            GameController.instance.EndLevel();
        }
    }

    private void Respawn()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.position = crateSpawn.transform.position;
        this.transform.rotation = crateSpawn.transform.rotation;
        crateState = CRATESTATE_dup.MOVING;
    }

    public void Reset()
    {
        crateSpawn = GameObject.Find("Cube Spawn 1");
        checkPoints.Clear();
        Respawn();
    }

    public CRATESTATE_dup GetCrateState()
    {
        return crateState;
    }
}
