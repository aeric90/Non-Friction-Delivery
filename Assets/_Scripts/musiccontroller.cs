using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiccontroller : MonoBehaviour
{
    public static musiccontroller instance;

    private AudioSource musicSource;
    public AudioClip[] titleLoops;
    public AudioClip[] levelLoops;
    private int trackNo = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;    
        musicSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameController.instance.getGameState())
        {
            case GAMESTATE.START:
                TitleMusic();
                break;
            case GAMESTATE.RUN:
            case GAMESTATE.LEVEL_START:
                LevelMusic();
                break;
            case GAMESTATE.LEVEL_END:
                LevelEndMusic();
                break;
            default:
                trackNo = 0;
                break;
        }
    }

    public void TitleMusic()
    {
        if(!musicSource.isPlaying)
        {
            if (trackNo >= titleLoops.Length) trackNo = 0;
            musicSource.clip = titleLoops[trackNo];
            musicSource.Play();

            trackNo++;
        }
    }

    private void LevelMusic()
    {
        if(!musicSource.isPlaying)
        {
            if (trackNo >= levelLoops.Length - 1) trackNo = 0;
            musicSource.clip = levelLoops[trackNo];
            musicSource.Play();

            trackNo++;
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void LevelEndMusic()
    {
        musicSource.Stop();
        musicSource.PlayOneShot(levelLoops[levelLoops.Length - 1]);
        trackNo = 0;
     }
}
