using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 Velocity;
    public float MovementSpeed = 5.0f;
    public float RotationSpeed = 3.0f;
    public float CameraSpeed = 3.0f;

    public GameObject cameraRig;
    private InputReader InputReader;
    private Rigidbody PhysicsBody;

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
        ApplyGravity();
        Move();
        UpdateCamera();
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
