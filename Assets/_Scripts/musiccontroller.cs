using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiccontroller : MonoBehaviour
{
    private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameController.instance.getGameState())
        {
            case GAMESTATE.START:
                
                playMusic();
                break;
        }
    }

    private void playMusic()
    {
        if(!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}
