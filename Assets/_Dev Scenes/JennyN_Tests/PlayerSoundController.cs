using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    BoxCollider boxCollider;
    GameController gameController;
    PlayerController playerController;
    InputReader inputReader;
    public AudioClip walkOnConcrete, walkOnIce, runOnConcrete, runOnIce, sliding, floorSqueak;
    public AudioSource audioSource;
    RaycastHit hit;
    float distance = 100f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        playerController = GetComponent<PlayerController>();
        inputReader = GetComponent<InputReader>();
    }

    // Update is called once per frame
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
            if (GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                
                if (boxCollider != null && ((boxCollider.sharedMaterial.name == "ice_physics_mat_test") || (boxCollider.sharedMaterial.name == "ice_physics_mat_test (Instance)")))
                {
                    if ((playerController.playerState == PLAYERSTATE.MOVING))
                    {
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = runOnIce;
                            audioSource.Play();
                        }
                        if (playerController.moveDirection == Vector3.zero)
                        {
                            audioSource.Stop();
                            audioSource.clip = floorSqueak;
                            audioSource.volume = 0.1f;
                            audioSource.Play();
                        }
                    }

                    if ((playerController.playerState == PLAYERSTATE.PUSHING))
                    {
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = walkOnIce;
                            audioSource.Play();
                        }
                        if (playerController.moveDirection == Vector3.zero)
                        {
                            audioSource.Stop();
                        }
                    }

                    if ((playerController.playerState == PLAYERSTATE.TEETERING) && (playerController.playerState != PLAYERSTATE.MOVING))
                    {
                        if (audioSource.clip == runOnIce || audioSource.clip == floorSqueak)
                        {
                            audioSource.Stop();
                        }
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = sliding;
                            audioSource.volume = 0.1f;
                            audioSource.Play();
                        }
                    }
                }

                if (boxCollider != null && ((boxCollider.sharedMaterial.name == "floor_physics_mat_test") || (boxCollider.sharedMaterial.name == "floor_physics_mat_test (Instance)")))
                {
                    if ((playerController.playerState == PLAYERSTATE.MOVING))
                    {
                        Debug.Log(playerController.moveDirection);
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = runOnConcrete;
                            audioSource.Play();
                        }
                        if (playerController.moveDirection == Vector3.zero)
                        {
                            audioSource.Stop();
                        }
                    }

                    if ((playerController.playerState == PLAYERSTATE.PUSHING))
                    {
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = walkOnConcrete;
                            audioSource.Play();
                        }
                        if (playerController.moveDirection == Vector3.zero)
                        {
                            audioSource.Stop();
                        }
                    }
                }
            }

            if (GetComponent<Rigidbody>().velocity.magnitude < 0.5 || (hit.transform != null && (hit.transform.tag == "falling")) || (hit.transform != null && (hit.transform.tag == "destroy")))
            {
                audioSource.Stop();
            }
        }

        if (gameController.getGameState() == GAMESTATE.LEVEL_END)
        {
            audioSource.Stop();
        }
    }
}
