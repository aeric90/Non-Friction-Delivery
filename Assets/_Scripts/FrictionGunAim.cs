using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionGunAim : MonoBehaviour
{
    public float gunRange = 75.0f;

    private GameObject aimingCell = null;
    private GameObject hitCell = null;
    public LayerMask layerMask;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = Camera.main.transform;
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunRange, layerMask) && hit.transform.tag == "GridCell")
        {
            hitCell = hit.transform.gameObject;
            if (aimingCell != null && aimingCell != hitCell) aimingCell.GetComponent<Outline>().enabled = false;
            aimingCell = hitCell;
            aimingCell.GetComponent<Outline>().enabled = true;
        } else
        {
            if (aimingCell != null) aimingCell.GetComponent<Outline>().enabled = false;
        }
    }

    public void ShootCell()
    {
        if(aimingCell != null)
        {
            aimingCell.GetComponent<GridCell>().GetShot();
        }
    }

    public void ClearAim()
    {
        aimingCell = null;
    }
}
