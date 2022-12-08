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
    private AudioSource laserHitSound;
    private bool hit = false;
    public GameObject laserHitPrefab;

    private void Start()
    {
        laserHitSound = GetComponent<AudioSource>();
        startTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - startTime > lifeTime) Destroy(this.gameObject);
        if(hit && !laserHitSound.isPlaying) Destroy(this.gameObject);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, shotDirection, speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(shotDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GridCell") other.gameObject.GetComponent<GridCell>().GetShot();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        laserHitSound.Play();
        GameObject laserHit = Instantiate(laserHitPrefab);
        laserHit.transform.position = transform.position;

        hit = true;
    }
}
