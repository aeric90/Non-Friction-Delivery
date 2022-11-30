using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSoundController : MonoBehaviour
{
    MeshCollider cellCollider;
    GridCell gridCell;
    public AudioClip crateOnConcrete, crateOnIce;
    private AudioSource audioSource;

    void Start()
    {
        gridCell = GameObject.Find("Changeable Floor").GetComponent<GridCell>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GridCell")
        {
            cellCollider = other.gameObject.GetComponent<MeshCollider>();
        }
    }

    void Update()
    {
        if (GetComponent<CrateController_dup>().GetCrateState() == CRATESTATE_dup.MOVING)
        {
            if ((cellCollider.material == gridCell.icePhysMat) && GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                audioSource.clip = crateOnIce;
                audioSource.Play();
            }
            else if ((cellCollider.material == gridCell.normalPhysMat) && GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                audioSource.clip = crateOnConcrete;
                audioSource.Play();
            }
        }
    }
}
