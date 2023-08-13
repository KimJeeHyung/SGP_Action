using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageProgress : MonoBehaviour
{

    [SerializeField]
    private Slider progress_slider;


    public LevelControl level_control;

    [SerializeField]
    private GameRoot game_root;

    // Update is called once per frame
    void Update()
    {
        progress_slider.value = game_root.getPlayTime() / level_control.GetLevelDatas()[3].end_time;
    }


}
