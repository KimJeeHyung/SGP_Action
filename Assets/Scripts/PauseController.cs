using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject Pause_Panel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Pause_Panel.activeSelf == false)
        {
            Pause_Panel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && Pause_Panel.activeSelf == true)
        {
            Pause_Panel.SetActive(false);
            Time.timeScale = 1.0f;
        }

    }

    public void Exit_Panel()
    {
        Pause_Panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
