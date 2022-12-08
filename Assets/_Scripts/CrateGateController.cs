using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateGateController : MonoBehaviour
{
    public BoxCollider centerCollider;

    void Start()
    {
        Collider[] crateColiders = CrateController.instance.GetColiders();

        foreach (Collider c in crateColiders)
        {
            Physics.IgnoreCollision(centerCollider, c, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
