using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{

    [SerializeField]
    private GameObject how_panel;

    public float how_index = 2;


    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void HowTo()
    {
        how_panel.SetActive(true);
    }

    public void How_to_exit()
    {
        how_panel.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        SoundManager.instance.StopAllCoroutines();
        SoundManager.instance.StartCoroutine("PlayBGM","1STAGE");
    }
}
