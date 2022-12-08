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

    public float lowAngle= 60;
    public float highAngle = 280;

    public bool jump;

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

        if (angle > 180 && angle < highAngle)
        {
            angles.x = highAngle;
        }
        else if (angle < 180 && angle > lowAngle)
        {
            angles.x = lowAngle;
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
        look = context.ReadValue<Vector2>().normalized * mouse_look_sensitivty;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switch (GameController.instance.getGameState())
            {
                case GAMESTATE.RUN:
                    GetComponent<FrictionGunAim>().ShootCell();
                    playerAnimator.SetTrigger("shoot");
                    PlayerController.instance.playerShoot();
                    break;
            }
        } 
        else if (context.canceled)
        {
            playerAnimator.ResetTrigger("shoot");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switch (GameController.instance.getGameState())
            {
                case GAMESTATE.START:
                    GameController.instance.StartGame();
                    break;
                case GAMESTATE.RUN:
                    PlayerController.instance.DoJump();
                    break;
                case GAMESTATE.LEVEL_START:
                    GameController.instance.StartLevel();
                    break;
                case GAMESTATE.END:
                    GameController.instance.ResetGame();
                    break;
            }
        }
    }

    public void OnDetonate(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            //GameController.instance.EndLevel();
            CrateController.instance.DestroyCrate();
        }
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switch (GameController.instance.getGameState())
            {
                case GAMESTATE.START:
                    Application.Quit();
                    break;
                case GAMESTATE.RUN:
                case GAMESTATE.LEVEL_START:
                case GAMESTATE.END:
                    GameController.instance.ResetGame();
                    break;
            }
        }
    }
}
