using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterControl : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator�� �����ϴ� ����.


    public PlayerStat Player_Stat; // �÷��̾� ������ �����ϴ� PlayerStat ��ũ��Ʈ


    private Slider Player_HP;

    // Start is called before the first frame update
    void Start()
    {

        Player_Stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        // MapCreator�� �����ͼ� ��� ���� map_creator�� ����.
        map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶󿡰� �� �Ⱥ��̳İ� ����� �� ���δٰ� ����ϸ�,
        if (this.map_creator.isDelete(this.gameObject))
        {
            GameObject.Destroy(this.gameObject); // �ڱ� �ڽ��� ����.
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Stat.HP -= 10;
            Debug.Log("�÷��̾��� ���� ü����" + Player_Stat.HP + "�÷��̾��� ���� �ִ� ü����" + Player_Stat.MAX_HP);
        }
    }

   
}
