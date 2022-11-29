using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    static public playerAnimationController instance;
    private Animator playerAnimator;
    public float turnSpeed = 5.0f;
    private GameObject rotateTarget;

    private void Start()
    {
        instance = this;
        rotateTarget = this.gameObject;
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

        var step = turnSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTarget.transform.rotation, step);
    }

    public void ClearTarger()
    {
        
    }

    public void RotateTarget(GameObject target)
    {
        rotateTarget = target;
    }
}
