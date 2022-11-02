using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraXMax = 0.0f;
    public float CameraXMin = 40.0f;

    // Update is called once per frame
    void Update()
    {
        float x = transform.localEulerAngles.x;

        if (x < CameraXMax) Camera.main.transform.localEulerAngles = new Vector3(CameraXMax, Camera.main.transform.localEulerAngles.y, 0);
        if (x > CameraXMin && x < 355.0f) Camera.main.transform.localEulerAngles = new Vector3(CameraXMin, Camera.main.transform.localEulerAngles.y, 0);
    }
}
