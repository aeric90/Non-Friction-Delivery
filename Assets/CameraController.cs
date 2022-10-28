using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.x < 5.0f) transform.eulerAngles = new Vector3(5.0f, 0.0f, 0.0f);
        if (transform.rotation.eulerAngles.x > 30.0f) transform.eulerAngles = new Vector3(30.0f, 0.0f, 0.0f);
    }
}
