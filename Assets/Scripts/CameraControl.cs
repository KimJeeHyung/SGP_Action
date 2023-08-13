using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject player = null;
    private GameObject boss = null;
    private Vector3 position_offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // ��� ���� player�� Player ������Ʈ�� �Ҵ�.
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.boss = GameObject.FindGameObjectWithTag("Boss");
        // ī�޶� ��ġ(this.transform.position)�� �÷��̾� ��ġ(this.player.transform.position)�� ����.
        this.position_offset = this.transform.position - this.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ��� ���� ������Ʈ�� Update() �޼��� ó�� �Ŀ� �ڵ����� ȣ��.
    void LateUpdate()
    {
        // ī�޶� ���� ��ġ�� new_position�� �Ҵ�.
        Vector3 new_position = this.transform.position;
        // �÷��̾��� X��ǥ�� ���� ���� ���ؼ� new_position�� X�� ����.
        new_position.x = this.player.transform.position.x + this.position_offset.x;
        // ī�޶� ��ġ�� ���ο� ��ġ(new_position)�� ����.
        this.transform.position = new_position;
    }

    public void BossAttackSequence()
    {
        Time.timeScale = 0.05f;
        Vector3 sequencePos = new Vector3(player.transform.position.x + Vector3.Distance(boss.transform.position, player.transform.position), player.transform.position.y, -10f);
        transform.position = sequencePos;
    }

    public void ResetPosition()
    {
        Vector3 new_position;
        new_position.x = player.transform.position.x + position_offset.x;
        new_position.y = 0f;
        new_position.z = -10f;

        transform.position = new_position;
    }
}
