using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionGunShoot : MonoBehaviour
{
    FrictionGunAim aimingScript;
    GameObject grid;

    // Start is called before the first frame update
    void Start()
    {
        aimingScript = GameObject.Find("Player").GetComponent<FrictionGunAim>();
        grid = GameObject.Find("Grid");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (aimingScript.aiming && Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < grid.GetComponent<GridGenerator>().cells.Length; i++)
            {
                if (i == aimingScript.hoveredOverCellNum)
                {
                    grid.GetComponent<GridGenerator>().cells[i].GetComponent<GridCell>().isShot = true;
                    grid.GetComponent<GridGenerator>().cells[i].GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        */
    }
}
