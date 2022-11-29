using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool isShot = false;

    public PhysicMaterial normalPhysMat;
    public PhysicMaterial icePhysMat;

    public Material normalColorMat;
    public Material iceColorMat;

    public GameObject tileModel;

    private Renderer cellRenderer;
    private BoxCollider cellCollider;

    // Start is called before the first frame update
    void Start()
    {
        cellRenderer = tileModel.GetComponent<Renderer>();
        cellCollider = tileModel.GetComponent<BoxCollider>();
    }

    public void GetShot()
    {
        if (isShot)
        {
            isShot = false;
            cellCollider.material = normalPhysMat;
            cellRenderer.material = normalColorMat;
        } else
        {
            isShot = true;
            cellCollider.material = icePhysMat;
            cellRenderer.material = iceColorMat;
        }
    }

    public void Reset()
    {
        isShot = false;
        cellCollider.material = normalPhysMat;
        cellRenderer.material = normalColorMat;
    }
}
