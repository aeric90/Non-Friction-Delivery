using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionGunAim : MonoBehaviour
{
    GameObject grid;
    public int hoveredOverCellNum;
    public bool aiming;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid");
    }

    // Update is called once per frame
    void Update()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit) && hit.transform.tag == "GridCell")
        {
            aiming = true;
            hoveredOverCellNum = hit.transform.GetComponent<GridCell>().cellNum;
            //Debug.Log(hoveredOverCellNum);
        }
        else
        {
            aiming = false;
        }

        if (aiming)
        {
            for (int i = 0; i < grid.GetComponent<GridGenerator>().cells.Length; i++)
            {
                if ((i == hoveredOverCellNum) && grid.GetComponent<GridGenerator>().cells[i].GetComponent<GridCell>().isShot == false)
                {
                    grid.GetComponent<GridGenerator>().cells[i].GetComponent<Renderer>().material.color = Color.green;
                }
                else if ((i != hoveredOverCellNum) && grid.GetComponent<GridGenerator>().cells[i].GetComponent<GridCell>().isShot == false)
                {
                    grid.GetComponent<GridGenerator>().cells[i].GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
        else
        {
            for (int i = 0; i < grid.GetComponent<GridGenerator>().cells.Length; i++)
            {
                if (grid.GetComponent<GridGenerator>().cells[i].GetComponent<GridCell>().isShot == false)
                {
                    grid.GetComponent<GridGenerator>().cells[i].GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }
}
