using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PLAYERSTATE
{
    MOVING,
    PUSHING,
    RIDING
}

public class PlayerController : MonoBehaviour
{
    public Vector3 Velocity;
    public float MovementSpeed = 5.0f;
    public float RotationSpeed = 3.0f;
    public float CameraSpeed = 3.0f;

    public GameObject cameraRig;
    private InputReader InputReader;
    private Rigidbody PhysicsBody;

    private PLAYERSTATE playerState = PLAYERSTATE.MOVING;

    // Start is called before the first frame update
    void Start()
    {
        //MainCamera = Camera.main.transform;

        InputReader = GetComponent<InputReader>();
        PhysicsBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMoveDirection();
        ChangeFaceDirection();
        ChangeCameraDirection();
        UpdateCamera();

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
            case PLAYERSTATE.RIDING:
                // Player can shoot in this state
                // Movement is translated over to the crate as a "lean"

                // If the ride button is release
                    // Change the player's state to MOVING
                    // ^ Unparent the player from the crate and reset it's position
                break;
            default:
                break;
        }
    }

    protected void CalculateMoveDirection()
    {
        Vector3 cameraForward = new(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 cameraRight = new(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

        Vector3 moveDirection = cameraForward.normalized * InputReader.m_move.y + cameraRight.normalized * InputReader.m_move.x;

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

    protected void ChangeFaceDirection()
    {
        float angle = InputReader.m_look.x * 100 * RotationSpeed;

        cameraRig.transform.Rotate(new Vector3(0.0f, angle, 0.0f) * Time.deltaTime);

    }

    protected void ChangeCameraDirection()
    {
        float angle = InputReader.m_look.y * 100 * CameraSpeed;

        Camera.main.transform.Rotate(new Vector3(angle, 0.0f, 0.0f) * Time.deltaTime);
    }

    private void UpdateCamera()
    {
        cameraRig.transform.position = this.transform.position;
    }
}
