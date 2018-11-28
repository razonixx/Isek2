using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Splashscreen : MonoBehaviour {
    public Font myFont;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUIStyle overlayText = new GUIStyle(GUI.skin.label);
        overlayText.font = myFont;
        overlayText.fontSize = 175;

        GUIStyle overlayText2 = new GUIStyle(GUI.skin.label);
        overlayText2.font = myFont;
        overlayText2.fontSize = 155;

        GUIStyle overlayText3 = new GUIStyle(GUI.skin.label);
        overlayText3.font = myFont;
        overlayText3.fontSize = 225;

        GUI.Label(new Rect(Screen.width / 12, Screen.height / 2 - Screen.height / 3 - Screen.height / 6, 2050, 2000), "The Restrainment", overlayText);
        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 45, Screen.height / 2 - Screen.height / 3, 550, 200), "of", overlayText2);
        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 9, Screen.height / 2 - Screen.height / 4 + Screen.height / 33, 650, 200), "ISEK", overlayText3);


        if (GUI.Button(new Rect(Screen.width / 4,                    Screen.height / 2 + Screen.height / 8, 150, 40), "Play game (Normal)"))
        {
            SceneManager.LoadScene("Normal");
        }
        if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 8, Screen.height / 2 + Screen.height / 8, 150, 40), "Play game (Fast)"))
        {
            SceneManager.LoadScene("Fast");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 32, Screen.height / 2 + Screen.height / 3, 90, 40), "Exit"))
        {
            Application.Quit();
        }
    }
}
