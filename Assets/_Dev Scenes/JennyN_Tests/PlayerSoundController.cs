using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    MeshCollider cellCollider;
    GridCell gridCell;
    public AudioClip walkOnConcrete, walkOnIce, runOnConcrete, runOnIce;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gridCell = GameObject.Find("Changeable Floor").GetComponent<GridCell>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
