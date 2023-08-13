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
        // 멤버 변수 player에 Player 오브젝트를 할당.
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.boss = GameObject.FindGameObjectWithTag("Boss");
        // 카메라 위치(this.transform.position)와 플레이어 위치(this.player.transform.position)의 차이.
        this.position_offset = this.transform.position - this.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 모든 게임 오브젝트의 Update() 메서드 처리 후에 자동으로 호출.
    void LateUpdate()
    {
        // 카메라 현재 위치를 new_position에 할당.
        Vector3 new_position = this.transform.position;
        // 플레이어의 X좌표에 차이 값을 더해서 new_position의 X에 대입.
        new_position.x = this.player.transform.position.x + this.position_offset.x;
        // 카메라 위치를 새로운 위치(new_position)로 갱신.
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
