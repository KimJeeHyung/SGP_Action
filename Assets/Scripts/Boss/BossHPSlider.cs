using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPSlider : MonoBehaviour
{
    [SerializeField]
    private Slider boss_hp = null;

    private BossControl boss_control = null;

    // Start is called before the first frame update
    void Start()
    {
        boss_control = FindObjectOfType<BossControl>();
    }

    // Update is called once per frame
    void Update()
    {
        float current_hp = boss_control.HP;
        if (current_hp <= 0f)
        {
            current_hp = 0f;
        }

        boss_hp.value = current_hp / boss_control.MaxHP;
    }
}
