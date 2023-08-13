using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireballAttack : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator�� �����ϴ� ����.
    public BossMapCreator boss_map_creator = null; // MapCreator�� �����ϴ� ����.

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // MapCreator�� �����ͼ� ��� ���� map_creator�� ����.
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            boss_map_creator = GameObject.Find("GameRoot").GetComponent<BossMapCreator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // ī�޶󿡰� �� �Ⱥ��̳İ� ����� �� ���δٰ� ����ϸ�,
            if (this.map_creator.isDelete(this.gameObject))
            {
                GameObject.Destroy(this.gameObject); // �ڱ� �ڽ��� ����.
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            // ī�޶󿡰� �� �Ⱥ��̳İ� ����� �� ���δٰ� ����ϸ�,
            if (this.boss_map_creator.isDelete(this.gameObject))
            {
                GameObject.Destroy(this.gameObject); // �ڱ� �ڽ��� ����.
            }
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStat.Instance.SetHP(-10.0f);
        }
    }
}
