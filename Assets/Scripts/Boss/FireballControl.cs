using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballControl : MonoBehaviour
{
    public BossMapCreator map_creator = null; // MapCreator�� �����ϴ� ����.

    private GameObject player = null;
    private Vector3 targetVec;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // MapCreator�� �����ͼ� ��� ���� map_creator�� ����.
        map_creator = GameObject.Find("GameRoot").GetComponent<BossMapCreator>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        AngleSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetVec * speed * Time.deltaTime;

        // ī�޶󿡰� �� �Ⱥ��̳İ� ����� �� ���δٰ� ����ϸ�,
        if (this.map_creator.isDelete(this.gameObject))
        {
            GameObject.Destroy(this.gameObject); // �ڱ� �ڽ��� ����.
        }
    }

    void AngleSetUp()
    {
        targetVec = (player.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStat.Instance.SetHP(-10);
        }
    }
}
