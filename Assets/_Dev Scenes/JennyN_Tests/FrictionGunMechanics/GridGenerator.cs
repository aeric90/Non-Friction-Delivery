using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int width = 1;
    public int height = 1;

    public GameObject cellPrefab;
    public GameObject[] cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = new GameObject[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = x * 2f;
        position.y = 0f;
        position.z = z * 2f;

        GameObject cell = cells[i] = Instantiate<GameObject>(cellPrefab);
        cell.GetComponent<GridCell>().cellNum = i;
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
