using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVel = transform.InverseTransformDirection(PlayerController.instance.Velocity);
        Vector2 localMove = InputReader.instance.move;

        playerAnimator.SetFloat("speed", Mathf.Abs(localVel.z));
        playerAnimator.SetFloat("x", localMove.x);
        playerAnimator.SetFloat("z", localMove.y);
        playerAnimator.SetBool("push", PlayerController.instance.playerState == PLAYERSTATE.PUSHING);
        playerAnimator.SetBool("jump", PlayerController.instance.playerState == PLAYERSTATE.JUMPING);
        playerAnimator.SetBool("teeter", PlayerController.instance.playerState == PLAYERSTATE.TEETERING);
    }
}
