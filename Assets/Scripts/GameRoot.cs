using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour
{
    public float step_timer = 0.0f; // 경과 시간을 유지한다.
    private PlayerControl player = null;
    public LevelControl level_control = null;

    [SerializeField]
    private GameObject Shop_UI;

    public bool Shop_Available = false;

    // Start is called before the first frame update
    void Start()
    {
        Shop_UI.SetActive(false);
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.stage_cleared == false)
        {
            this.step_timer += Time.deltaTime;  // 경과 시간을 더해 간다.
        }

        if (this.player.isPlayEnd())
        {
            if (PlayerStat.Instance.GetReviveNum() != 0)
            {
                Debug.Log("부활 !");
                this.player.step = PlayerControl.STEP.JUMP;
                player.Revive();
                StartCoroutine(player.Text_Fade_In("부활 !"));
            }
            else
            {           
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    public void Shop_Open()
    {
        Shop_UI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public float getPlayTime()
    {
        float time;
        time = this.step_timer;
        return (time);  // 호출한 곳에 경과 시간을 알려준다.
    }
}
