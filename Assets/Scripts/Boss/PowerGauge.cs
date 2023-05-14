using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGauge : MonoBehaviour
{
    private Slider slider = null;
    private GameObject player = null;
    private BossPlayerControl player_control = null;
    public GameObject fill_area = null;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player_control = player.GetComponent<BossPlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player_control.current_power / (float)player_control.max_power;

        if(slider.value<=0)
        {
            fill_area.SetActive(false);
        }
        else
        {
            fill_area.SetActive(true);
        }

        slider.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0f, 1f, 0f));
    }
}
