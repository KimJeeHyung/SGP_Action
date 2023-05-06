using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private GUIStyle guiStyle = new GUIStyle();

    void OnGUI()
    {
        guiStyle.fontSize = 80;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(Screen.width / 2 - 240, Screen.height / 2 - 40, 128, 32), "Plastic Runner", guiStyle);
    }
}
