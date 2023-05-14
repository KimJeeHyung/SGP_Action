using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGameRoot : MonoBehaviour
{
    public float step_timer = 0.0f; // ��� �ð��� �����Ѵ�.
    private BossPlayerControl player = null;
    public LevelControl level_control = null;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<BossPlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.stage_cleared == false)
        {
            this.step_timer += Time.deltaTime; // ��� �ð��� ���� ����.
        }

        if (this.player.isPlayEnd())
        {
            SceneManager.LoadScene("BossScene");
        }
    }

    public float getPlayTime()
    {
        float time;
        time = this.step_timer;
        return (time);  // ȣ���� ���� ��� �ð��� �˷��ش�.
    }
}
