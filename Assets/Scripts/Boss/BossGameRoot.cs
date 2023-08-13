using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGameRoot : MonoBehaviour
{
    public float step_timer = 0.0f; // 경과 시간을 유지한다.
    private BossPlayerControl player = null;
    private BossControl boss = null;
    public LevelControl level_control = null;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<BossPlayerControl>();
        boss = FindObjectOfType<BossControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.stage_cleared == false)
        {
            this.step_timer += Time.deltaTime; // 경과 시간을 더해 간다.
        }

        if (this.player.isPlayEnd())
        {
            if (PlayerStat.Instance.GetReviveNum() != 0)
            {
                Debug.Log("부활 !");
                this.player.step = BossPlayerControl.STEP.JUMP;
                player.Revive();
            }
            else
            {
                SceneManager.LoadScene("TitleScene");
            }
        }

        if (boss.HP <= 0f)
        {     
            FirebaseDatabaseController.OnSave();
            SceneManager.LoadScene("ClearScene");      
        }
    }

    public float getPlayTime()
    {
        float time;
        time = this.step_timer;
        return (time);  // 호출한 곳에 경과 시간을 알려준다.
    }
}
