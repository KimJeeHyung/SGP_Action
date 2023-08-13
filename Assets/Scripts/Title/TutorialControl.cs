using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialControl : MonoBehaviour
{
    [SerializeField]
    private GameObject how_panel;

    [SerializeField]
    private GameObject[] tutorials;
    public int how_index = 0;

    [SerializeField]
    private GameObject prev_button;
    [SerializeField]
    private GameObject next_button;

    [SerializeField]
    private GameObject info_panel;

    [SerializeField]
    private GameObject ranking_panel;

    [SerializeField]
    private GameObject ranking_Content;


    private void Update()
    {
        if(how_index == 0)
        {
            prev_button.SetActive(false);
        }
        else
        {
            prev_button.SetActive(true);
        }

        if (how_index == tutorials.Length - 1)
        {
            next_button.SetActive(false);
        }
        else
        {
            next_button.SetActive(true);
        }
    }

    public void StartGame()
    {
        RankingTimer.instance.InitTime();
        PlayerStat.Instance.ResetStat();
        SceneManager.LoadScene("GameScene");
        SoundManager.instance.StopAllCoroutines();
        SoundManager.instance.StartCoroutine("PlayBGM", "1STAGE");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenTutorial()
    {
        how_panel.SetActive(true);
    }

    public void CloseTutorial()
    {
        how_panel.SetActive(false);
    }

    public void NextPage()
    {
        how_index++;

        tutorials[how_index - 1].SetActive(false);
        tutorials[how_index].SetActive(true);
    }

    public void PrevPage()
    {
        how_index--;

        tutorials[how_index + 1].SetActive(false);
        tutorials[how_index].SetActive(true);
    }

    public void OpenInfo()
    {
        info_panel.SetActive(true);
    }

    public void CloseInfo()
    {
        info_panel.SetActive(false);
    }

    public void CloseRanking()
    {
        int count = ranking_Content.transform.childCount;
        for(int i = 0; i < count; ++i)
        {
            Destroy(ranking_Content.transform.GetChild(i).gameObject);
        }
        ranking_panel.SetActive(false);
    }
}
