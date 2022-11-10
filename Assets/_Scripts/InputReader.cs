using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputReader : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;
    public Vector2 mouse_look;

    public float look_sensitivty = 300.0f;
    public float mouse_look_sensitivty = 300.0f;


    public bool ride;

    public GameObject cameraRig;

    private void Start()
    {

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
                    break;
                case GAMESTATE.END:
                    GameController.instance.ResetGame();
                    GameController.instance.setGameState(GAMESTATE.START);
                    break;
            }
        }
    }
}
