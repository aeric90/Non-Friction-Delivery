using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    static public playerAnimationController instance;
    private Animator playerAnimator;
    public float turnSpeed = 500.0f;
    public GameObject crateObject;

    private void Start()
    {
        instance = this;
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
        playerAnimator.SetBool("fall", PlayerController.instance.playerState == PLAYERSTATE.FALLING);
        playerAnimator.SetBool("dead", PlayerController.instance.playerState == PLAYERSTATE.DEAD);
        playerAnimator.SetBool("ride", PlayerController.instance.playerState == PLAYERSTATE.RIDING);
    }

    private void FixedUpdate()
    {
        float rotation = 0.0f;
        Quaternion lookRotation = Quaternion.LookRotation(Camera.main.transform.forward);

        if (PlayerController.instance.playerState == PLAYERSTATE.PUSHING)
        {
            Vector3 localCrate = transform.InverseTransformDirection(crateObject.transform.position - transform.position);
            lookRotation = Quaternion.LookRotation(localCrate);
        }

        rotation = lookRotation.eulerAngles.y;
        Quaternion rotationQuat = Quaternion.Euler(0.0f, rotation, 0.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationQuat, Time.deltaTime);
    }
}
