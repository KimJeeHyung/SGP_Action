using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterControl : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator를 보관하는 변수.


    public PlayerStat Player_Stat; // 플레이어 스탯을 관리하는 PlayerStat 스크립트


    private Slider Player_HP;

    // Start is called before the first frame update
    void Start()
    {

        Player_Stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        // MapCreator를 가져와서 멤버 변수 map_creator에 보관.
        map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라에게 나 안보이냐고 물어보고 안 보인다고 대답하면,
        if (this.map_creator.isDelete(this.gameObject))
        {
            GameObject.Destroy(this.gameObject); // 자기 자신을 삭제.
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Stat.HP -= 10;
            Debug.Log("플레이어의 현재 체력은" + Player_Stat.HP + "플레이어의 현재 최대 체력은" + Player_Stat.MAX_HP);
        }
    }

   
}
