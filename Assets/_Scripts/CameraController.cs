using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraXMax = 0.0f;
    public float CameraXMin = 40.0f;
    private float lastX;

    // Update is called once per frame
    void Update()
    {
        /*
        float x = transform.localEulerAngles.x;

        if(CameraXMax < 0.0f)
        {
            if (x > 360.0f + CameraXMax)
            {
                Debug.Log("SET MAX " + x);
                Camera.main.transform.localEulerAngles = new Vector3(360.0f + CameraXMax, Camera.main.transform.localEulerAngles.y, 0);
            }
            else if (x > CameraXMin && x < 360.0f + CameraXMax)
            {
                Camera.main.transform.localEulerAngles = new Vector3(CameraXMin, Camera.main.transform.localEulerAngles.y, 0);
            }
        } 
        else
        {
            if (x < CameraXMax) Camera.main.transform.localEulerAngles = new Vector3(CameraXMax, Camera.main.transform.localEulerAngles.y, 0);
            else if (x > CameraXMin) Camera.main.transform.localEulerAngles = new Vector3(CameraXMin, Camera.main.transform.localEulerAngles.y, 0);
        }

     */   
    }
}
