using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    // Start is called before the first frame update

    public float HP { get; set; } // 플레이어 체력
    public float MAX_HP { get; set; } // 플레이어 최대 체력

    public int Revive_Num; // 부활 횟수

    [SerializeField]
    private Slider PlayerHP;

    [SerializeField]
    private PlayerControl Player_Control; // 플레이어를 컨트롤 하는 PlayerControll 스크립트

    void Start()
    {
        Revive_Num = 0;
        MAX_HP = 100;
        HP = MAX_HP;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHP.value = HP / MAX_HP;
    }

    public void Revive()
    {
        if(Revive_Num != 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, 3.0f, this.transform.position.z);
            Revive_Num -= 1;
            Player_Control.ResetJumpCount();
            Debug.Log("부활 아이템이 자동적으로 사용되었으며 남은 부활 횟수는" + Revive_Num);
            
        }

    }


}
