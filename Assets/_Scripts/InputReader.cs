using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputReader : MonoBehaviour
{
    public Vector2 m_move;
    public Vector2 m_look;
    public bool b_ride;

    private void Start()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        m_look = context.ReadValue<Vector2>();
    }

    public void OnRide(InputAction.CallbackContext context)
    {
        b_ride = context.ReadValue<bool>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        switch(GameController.instance.getGameState())
        {
            case GAMESTATE.START:
                GameController.instance.setGameState(GAMESTATE.RUN);
                break;
            case GAMESTATE.RUN:
                GetComponent<FrictionGunAim>().ShootCell();
                break;
            case GAMESTATE.END:
                // RESET GAME TO START POSITION
                break;
        }
    }
}
