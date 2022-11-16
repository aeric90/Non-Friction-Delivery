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

        playerAnimator.SetFloat("speed", Mathf.Abs(localVel.z));
    }
}
