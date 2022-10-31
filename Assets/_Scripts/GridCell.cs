using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public int cellNum;
    public bool isShot = false;

    public PhysicMaterial normalPhysMat;
    public PhysicMaterial icePhysMat;

    public Material normalColorMat;
    public Material iceColorMat;
    public Material normalHighlightColorMat;
    public Material iceHighlightColorMat;

    private Renderer cellRenderer;
    private MeshCollider cellCollider;

    // Start is called before the first frame update
    void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        cellCollider = GetComponent<MeshCollider>();
    }

    public void AddHighlight()
    {
        if (isShot) cellRenderer.material = iceHighlightColorMat;
        else cellRenderer.material = normalHighlightColorMat;
    }

    public void RemoveHighlight()
    {
        if (isShot) cellRenderer.material = iceColorMat;
        else cellRenderer.material = normalColorMat;
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
}
