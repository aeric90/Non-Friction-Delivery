using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTextCrawlController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textCrawl;
    public float ySpeed = 0.5f;
    public float maxY = 1500.0f;
    private float startY;

    void Start()
    {
        startY = textCrawl.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = textCrawl.transform.position;
        float newY = curPos.y + ySpeed;

        if (newY >= maxY) newY = startY;
        textCrawl.transform.position = new Vector3(curPos.x, newY, curPos.z);
    }
}
