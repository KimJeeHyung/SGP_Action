using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerControl : MonoBehaviour
{
    public BossMapCreator map_creator = null; // MapCreator�� �����ϴ� ����.
    public BossPlayerControl player_control = null;

    // Start is called before the first frame update
    void Start()
    {
        // MapCreator�� �����ͼ� ��� ���� map_creator�� ����.
        map_creator = GameObject.Find("GameRoot").GetComponent<BossMapCreator>();
        player_control = FindObjectOfType<BossPlayerControl>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_control.current_power++;

            Destroy(this.gameObject);
        }
    }
}
