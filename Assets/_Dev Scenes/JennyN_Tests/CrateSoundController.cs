using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSoundController : MonoBehaviour
{
    BoxCollider boxCollider;
    GridCell gridCell;
    GameController gameController;
    public AudioClip crateOnConcrete, crateOnIce;
    public PhysicMaterial iceFloorPhysicsMat, normalFloorPhysicsMat;
    private AudioSource audioSource;

    void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "floor")
        {
            boxCollider = other.gameObject.GetComponent<BoxCollider>();
        }
    }

    void Update()
    {
        if ((GetComponent<CrateController_dup>().GetCrateState() == CRATESTATE_dup.MOVING) && (gameController.getGameState() == GAMESTATE.LEVEL_START))
        {
            if ((boxCollider.material == iceFloorPhysicsMat) && GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                audioSource.clip = crateOnIce;
                audioSource.Play();
            }
            else if ((boxCollider.material == normalFloorPhysicsMat) && GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                audioSource.clip = crateOnConcrete;
                audioSource.Play();
            }
        }
    }
}
