using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE
{
    MOVING,
    PUSHING,
    RIDING,
    DEAD
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Vector3 moveDirection;
    public Vector3 Velocity;
    public float MovementSpeed = 5.0f;
    public float RotationSpeed = 3.0f;
    public float CameraSpeed = 3.0f;
    public float look_sensitivty = 300.0f;

    public GameObject cameraRig;
    private InputReader InputReader;
    private Rigidbody PhysicsBody;

    private GameObject playerSpawn;
    private float deathTime;
    private float spawnTime = 2.0f;

    private float yMax = 0.5f;

    private List<int> checkPoints = new List<int>();

    public PLAYERSTATE playerState = PLAYERSTATE.MOVING;

    // Start is called before the first frame update
    void Start()
    {
        //MainCamera = Camera.main.transform;
        instance = this;
        InputReader = GetComponent<InputReader>();
        PhysicsBody = GetComponent<Rigidbody>();
        playerSpawn = GameObject.Find("Player Spawn 1");
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMoveDirection();
        UpdateCamera();
        
        // Cap Y position to prevent bouncing
        if(transform.position.y > yMax) transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
    }

    void FixedUpdate()
    {
        switch (playerState)
        {
            case PLAYERSTATE.MOVING:
                // Player can shoot in this state
                // If player is in contact with a crate and is moving "into" it
                    // Change the player's state to pushing
                ApplyGravity();
                Move();
                break;
            case PLAYERSTATE.PUSHING:
                // PLayer cannot shoot in this state
                ApplyGravity();
                Move();
                // If the ride button is currently held down and thee crate's velocity is at the ride point
                    // Change the player's state to riding
                    // ^ Parent the player to the crate and reset it's position
                break;
            case PLAYERSTATE.DEAD:
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                if(Time.time - deathTime >= spawnTime) Respawn();
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "crate")
        {
            Vector3 crateDir = collision.gameObject.transform.position - transform.position;
            Vector2 crateDir2d = new Vector2(crateDir.x, crateDir.z);
            Vector2 moveDir2d = new Vector2(moveDirection.x, moveDirection.z);
            float angle = Vector3.Angle(crateDir2d, moveDir2d);
            if(angle < 80.0f) playerState = PLAYERSTATE.PUSHING; else playerState = PLAYERSTATE.MOVING;
        }
        else
        {
            playerState = PLAYERSTATE.MOVING;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "crate")
        {
            playerState = PLAYERSTATE.MOVING;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroy")
        {
            deathTime = Time.time;
            playerState = PLAYERSTATE.DEAD;
        }
        else if(other.gameObject.tag == "checkpoint")
        {
            if(!checkPoints.Contains(other.gameObject.GetComponent<CheckpointController>().checkPointID))
            {
                checkPoints.Add(other.gameObject.GetComponent<CheckpointController>().checkPointID);
                playerSpawn = other.gameObject.GetComponent<CheckpointController>().playerSpawn;
            }
        }
    }

    protected void CalculateMoveDirection()
    {
        Vector3 cameraForward = new(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 cameraRight = new(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

        moveDirection = cameraForward.normalized * InputReader.move.y + cameraRight.normalized * InputReader.move.x;

        Velocity.x = moveDirection.x * MovementSpeed;
        Velocity.z = moveDirection.z * MovementSpeed;
    }

    protected void ApplyGravity()
    {
        if (Velocity.y > Physics.gravity.y)
        {
            Velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    protected void Move()
    {
        PhysicsBody.AddForce(Velocity);
    }

    private void UpdateCamera()
    {
        cameraRig.transform.position = this.transform.position;

        float angle = InputReader.look.y * look_sensitivty * Time.deltaTime;
        Vector3 newRotation = new Vector3(-angle, 0.0f, 0.0f);

        Camera.main.transform.localEulerAngles = Camera.main.transform.localEulerAngles + newRotation;

        angle = InputReader.look.x * look_sensitivty;
        cameraRig.transform.Rotate(new Vector3(0.0f, angle, 0.0f) * Time.deltaTime);
    }

    private void Respawn()
    {
        GetComponent<FrictionGunAim>().ClearAim();
        this.transform.position = playerSpawn.transform.position;
        this.transform.rotation = playerSpawn.transform.rotation;
        UpdateCamera();
        playerState = PLAYERSTATE.MOVING;
    }

    public void Reset()
    {
        playerSpawn = GameObject.Find("Player Spawn 1");
        checkPoints.Clear();
        Respawn();
    }
}
