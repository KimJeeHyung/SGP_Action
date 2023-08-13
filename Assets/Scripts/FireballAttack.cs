using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireballAttack : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator를 보관하는 변수.
    public BossMapCreator boss_map_creator = null; // MapCreator를 보관하는 변수.

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // MapCreator를 가져와서 멤버 변수 map_creator에 보관.
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
            // 카메라에게 나 안보이냐고 물어보고 안 보인다고 대답하면,
            if (this.map_creator.isDelete(this.gameObject))
            {
                GameObject.Destroy(this.gameObject); // 자기 자신을 삭제.
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            // 카메라에게 나 안보이냐고 물어보고 안 보인다고 대답하면,
            if (this.boss_map_creator.isDelete(this.gameObject))
            {
                GameObject.Destroy(this.gameObject); // 자기 자신을 삭제.
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
