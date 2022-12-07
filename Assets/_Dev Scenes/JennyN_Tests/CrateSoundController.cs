using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSoundController : MonoBehaviour
{
    BoxCollider boxCollider;
    GameController gameController;
    CrateController_dup2 crateController;
    public AudioClip crateOnConcrete, crateOnIce;
    public PhysicMaterial iceFloorPhysicsMat, normalFloorPhysicsMat;
    public AudioSource audioSource;
    RaycastHit hit;
    float distance = 100f;

    void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        crateController = GetComponent<CrateController_dup2>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
        {
            if (hit.transform.tag == "floor" || hit.transform.tag == "GridCell")
            {
                boxCollider = hit.collider.gameObject.GetComponent<BoxCollider>();
            }
        }

        if (gameController.getGameState() == GAMESTATE.RUN)
        {
            if (GetComponent<Rigidbody>().velocity != Vector3.zero && !audioSource.isPlaying)
            {
                if (boxCollider != null && ((boxCollider.sharedMaterial.name == "ice_physics_mat_test") || (boxCollider.sharedMaterial.name == "ice_physics_mat_test (Instance)")))
                {
                    audioSource.clip = crateOnIce;
                    audioSource.Play();
                }

                if (boxCollider != null && ((boxCollider.sharedMaterial.name == "floor_physics_mat_test") || (boxCollider.sharedMaterial.name == "floor_physics_mat_test (Instance)")))
                {
                    audioSource.clip = crateOnConcrete;
                    audioSource.Play();
                }
            }

            if (GetComponent<Rigidbody>().velocity.magnitude < 0.5 || (hit.transform != null && (hit.transform.tag == "falling")) || (hit.transform != null && (hit.transform.tag == "destroy")) || crateController.GetCrateState() != CRATESTATE_dup2.MOVING)
            {
                audioSource.Stop();
            }
        }
    }
}
