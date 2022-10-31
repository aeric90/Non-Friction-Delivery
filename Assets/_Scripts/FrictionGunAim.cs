using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionGunAim : MonoBehaviour
{
    public float gunRange = 75.0f;

    private GameObject aimingCell = null;
    private GameObject hitCell = null;

    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = Camera.main.transform;
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunRange) && hit.transform.tag == "GridCell")
        {
            hitCell = hit.transform.gameObject;
            if (aimingCell != null && aimingCell != hitCell) aimingCell.GetComponent<GridCell>().RemoveHighlight();
            aimingCell = hitCell;
            aimingCell.GetComponent<GridCell>().AddHighlight();
        }
    }

    public void ShootCell()
    {
        if(aimingCell != null)
        {
            aimingCell.GetComponent<GridCell>().GetShot();
        }
    }
}
