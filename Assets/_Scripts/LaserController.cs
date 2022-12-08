using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 shotDirection;
    public float distance = 1000.0f;
    private float startTime;
    public float lifeTime = 5.0f;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - startTime > lifeTime) Destroy(this.gameObject);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, shotDirection, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GridCell") other.gameObject.GetComponent<GridCell>().GetShot();
        Destroy(this.gameObject);
    }
}
