using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public float blowForce = 50.0f;
    Vector3 fanForward;

    private void Start()
    {
        fanForward = this.transform.right;
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if(rb != null)
        {
            rb.AddForce(fanForward * blowForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(fanForward * blowForce);
        }
    }
}
