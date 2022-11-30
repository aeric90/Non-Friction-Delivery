using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    static public LevelController instance; 
    public string tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
