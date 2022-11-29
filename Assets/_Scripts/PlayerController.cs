using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE
{
    MOVING,
    PUSHING,
    JUMPING,
    TEETERING,
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
    public float jump_velocity = 18.4f;
    public float current_gravity = Physics.gravity.y;
    public float jump_gravity_scale = 3.0f;

    public GameObject cameraRig;
    private InputReader InputReader;
    private Rigidbody PhysicsBody;

    private GameObject playerSpawn;
    private float deathTime;
    private float spawnTime = 2.0f;

    private bool yLock = true;
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
        int magnitudeValue = (int)new Vector2(PhysicsBody.velocity.x, PhysicsBody.velocity.z).magnitude;

        switch (playerState)
        {
            case PLAYERSTATE.MOVING:
                // Player can shoot in this state
                // Player can jump in this state
                // Player can push in this state
                if (magnitudeValue > 9) playerState = PLAYERSTATE.TEETERING;
                break;
            case PLAYERSTATE.PUSHING:
                // Player cannot jump in this state
                // Player cannot shoot in this state
                break;
            case PLAYERSTATE.TEETERING:
                // Player cannot shoot in this state
                if (magnitudeValue <= 9) playerState = PLAYERSTATE.MOVING;
                break;
            case PLAYERSTATE.JUMPING:
                // Player cannot jump in this state
                // Player cannot shoot in this state
                break;
            case PLAYERSTATE.DEAD:
                if (Time.time - deathTime >= spawnTime) Respawn();
                break;
            default:
                break;
        }

        // Cap Y position to prevent bouncing
        if (transform.position.y > yMax && yLock) transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
    }

    // Only for physics based calculations
    void FixedUpdate()
    {
        switch (playerState)
        {
            case PLAYERSTATE.MOVING:
            case PLAYERSTATE.PUSHING:
            case PLAYERSTATE.JUMPING:
            case PLAYERSTATE.TEETERING:
                ApplyGravity();
                Move();
                break;
            case PLAYERSTATE.DEAD:
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (playerState == PLAYERSTATE.PUSHING)
        {
            if (collision.gameObject.tag == "crate")
            {
                Vector3 crateDir = collision.gameObject.transform.position - transform.position;
                Vector2 crateDir2d = new Vector2(crateDir.x, crateDir.z);
                Vector2 moveDir2d = new Vector2(moveDirection.x, moveDirection.z);
                float angle = Vector3.Angle(crateDir2d, moveDir2d);
                if (angle < 80.0f) playerState = PLAYERSTATE.PUSHING; else playerState = PLAYERSTATE.MOVING;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "crate")
        {
            playerState = PLAYERSTATE.MOVING;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerState == PLAYERSTATE.JUMPING)
        {
            if (collision.gameObject.tag == "floor" || collision.gameObject.tag == "GridCell")
            {
                current_gravity = Physics.gravity.y;
                yLock = false;
                playerState = PLAYERSTATE.MOVING;
            }
        }
        if(playerState == PLAYERSTATE.MOVING)
        {
            if (collision.gameObject.tag == "crate")
            {
                Vector3 crateDir = collision.gameObject.transform.position - transform.position;
                Vector2 crateDir2d = new Vector2(crateDir.x, crateDir.z);
                Vector2 moveDir2d = new Vector2(moveDirection.x, moveDirection.z);
                float angle = Vector3.Angle(crateDir2d, moveDir2d);
                if (angle < 80.0f) playerState = PLAYERSTATE.PUSHING; else playerState = PLAYERSTATE.MOVING;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroy")
        {
            deathTime = Time.time;
            playerState = PLAYERSTATE.DEAD;
        }
        if(other.gameObject.tag == "checkpoint")
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
        if(InputReader.move.y < 0)
        {
            Velocity.z = moveDirection.z * (MovementSpeed * 0.8f);
        } else
        {
            Velocity.z = moveDirection.z * MovementSpeed;
        }

    }

    protected void ApplyGravity()
    {
        if (Velocity.y > current_gravity) Velocity.y += current_gravity;
        if (Velocity.y < current_gravity) Velocity.y = current_gravity;
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

    public void DoJump()
    {
        if(playerState == PLAYERSTATE.MOVING || playerState == PLAYERSTATE.TEETERING)
        {
            playerState = PLAYERSTATE.JUMPING;
            PhysicsBody.AddForce(new Vector3(0.0f, jump_velocity, 0.0f));
            current_gravity = Physics.gravity.y * jump_gravity_scale;
            yLock = false;
        }
    }
}
