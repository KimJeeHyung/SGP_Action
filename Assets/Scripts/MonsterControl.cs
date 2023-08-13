using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterControl : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator�� �����ϴ� ����.

    // Start is called before the first frame update
    void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStat.Instance.SetHP(-10f);
        }
    }
}
