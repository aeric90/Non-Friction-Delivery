using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDebrisController : MonoBehaviour
{
    public static CubeDebrisController instance;
    public List<GameObject> debrisList = new List<GameObject>();

    void Start()
    {
        instance = this;        
    }

    public void AddDebris(GameObject newDebris)
    {
        newDebris.transform.parent = transform;
        debrisList.Add(newDebris);

        if(debrisList.Count > 5)
        {
            Destroy(debrisList[0]);
            debrisList.RemoveAt(0);
        }
    }

    public void ClearDebris()
    {
        foreach(GameObject debris in debrisList) Destroy(debris);
        debrisList.Clear();
    }
}
