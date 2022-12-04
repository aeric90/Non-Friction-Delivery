using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

enum CRATESTATE
{
    MOVING,
    DEAD
}

public class CrateController : MonoBehaviour
{
    static public CrateController instance;

    public GameObject destroyedCratePrefab;

    public GameObject crateSpawn;
    private float deathTime;
    private float spawnTime = 3.0f;
    public Collider[] coliders;

    private List<int> checkPoints = new List<int>();

    private CRATESTATE crateState = CRATESTATE.MOVING;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        crateSpawn = GameObject.Find("Cube Spawn 1");
    }

    // Update is called once per frame
    void Update()
    {
        if (crateSpawn == null) crateSpawn = GameObject.Find("Cube Spawn 1");
        switch (crateState)
        {
            case CRATESTATE.MOVING:
                // Check collider contact for type of floor
                // Check velocity
                //    Play apporpriate sound effect
                break;
            case CRATESTATE.DEAD:
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
            DestroyCrate();
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
        GetComponent<MeshRenderer>().enabled = true;
        foreach (Collider c in coliders) c.enabled = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        this.transform.position = crateSpawn.transform.position;
        this.transform.rotation = crateSpawn.transform.rotation;
        crateState = CRATESTATE.MOVING;
    }

    public void Reset()
    {
        crateSpawn = GameObject.Find("Cube Spawn 1");
        checkPoints.Clear();
        Respawn();
    }

    public void DestroyCrate()
    {
        deathTime = Time.time;
        GetComponent<MeshRenderer>().enabled = false;
        foreach(Collider c in coliders) c.enabled = false;
        CubeDebrisController.instance.AddDebris(Instantiate(destroyedCratePrefab, transform.position, transform.rotation));
        crateState = CRATESTATE.DEAD;
    }

    public Collider[] GetColiders()
    {
        return coliders;
    } 
}
