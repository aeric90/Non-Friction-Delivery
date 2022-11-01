using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startPlayGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startScreen;
    public GameObject endScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPlay()
    {
        //  Debug.Log("false");
        // startScreen.SetActive(false);
        SceneManager.LoadScene("Eliana_test");

    }
}
