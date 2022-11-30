using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreenController : MonoBehaviour
{

    static public TutorialScreenController instance;

    public TMPro.TextMeshProUGUI tutorialText;
    public TMPro.TextMeshProUGUI levelText;

    void Start()
    {
        instance = this;  
    }

    public void UpdateTutorialText(string text)
    {
        tutorialText.text = text;
    }

    public void UpdateLevelText(string text)
    {
        levelText.text = text;
    }
}
