using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPSlider : MonoBehaviour
{
    [SerializeField]
    private Slider player_hp = null;

    // Update is called once per frame
    void Update()
    {
        float current_hp = PlayerStat.Instance.GetHP();
        if(current_hp <= 0f)
        {
            current_hp = 0f;
        }

        player_hp.value = current_hp / PlayerStat.Instance.GetMaxHP();
    }
}
