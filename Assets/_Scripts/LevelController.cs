using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    static public LevelController instance; 
    public string tutorialText;
    public float levelTime = 300;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
