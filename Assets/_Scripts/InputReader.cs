using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputReader : MonoBehaviour
{
    public static InputReader instance;

    public Vector2 move;
    public Vector2 look;
    public Vector2 mouse_look;

    public float look_sensitivty = 300.0f;
    public float mouse_look_sensitivty = 300.0f;

    public bool ride;

    public GameObject cameraRig;
    public GameObject followTransform;

    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;
    //public Camera camera;

    public Animator playerAnimator;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        followTransform.transform.rotation *= Quaternion.AngleAxis(look.x * rotationPower, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;
        //nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouse_look = context.ReadValue<Vector2>();

        float angle = mouse_look.y * mouse_look_sensitivty * Time.deltaTime;
        Vector3 newRotation = new Vector3(angle, 0.0f, 0.0f);

        Camera.main.transform.localEulerAngles = Camera.main.transform.localEulerAngles + newRotation;

        angle = mouse_look.x * mouse_look_sensitivty;
        cameraRig.transform.Rotate(new Vector3(0.0f, angle, 0.0f) * Time.deltaTime);
    }

    public void OnRide(InputAction.CallbackContext context)
    {
        ride = context.ReadValue<bool>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switch (GameController.instance.getGameState())
            {
                case GAMESTATE.START:
                    GameController.instance.setGameState(GAMESTATE.RUN);
                    break;
                case GAMESTATE.RUN:
                    GetComponent<FrictionGunAim>().ShootCell();
                    playerAnimator.SetTrigger("shoot");
                    break;
                case GAMESTATE.END:
                    GameController.instance.ResetGame();
                    GameController.instance.setGameState(GAMESTATE.START);
                    break;
            }
        } 
        else if (context.canceled)
        {
            playerAnimator.ResetTrigger("shoot");
        }
    }
}
