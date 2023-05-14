using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    private BossMapCreator map_creator = null;

    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;
    private float targetPosY = 0f;
    private Vector3 origin_pos = Vector3.zero;

    private Rigidbody rigid;
    private BoxCollider box_collider;

    public GameObject fireball = null;
    public bool skill_damaged = false;
    public bool stop_move = false;
    public bool reset_pos = false;

    // Start is called before the first frame update
    void Start()
    {
        map_creator = FindObjectOfType<BossMapCreator>();

        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        this.position_offset = this.transform.position - this.player.transform.position;
        origin_pos = Camera.main.WorldToViewportPoint(transform.position);

        rigid = GetComponent<Rigidbody>();
        box_collider = GetComponent<BoxCollider>();

        InvokeRepeating("PosYTracking", 5f, 5f);
        InvokeRepeating("CreateFireball", 5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (skill_damaged == true && map_creator.boss_pos != Vector3.zero)
        {
            rigid.isKinematic = true;
            stop_move = true;
            //transform.position = map_creator.boss_pos + new Vector3(0f, 0f, -1f);
            //Debug.Log("test");
        }
    }

    private void LateUpdate()
    {
        // 보스 현재 위치를 new_position에 할당.
        Vector3 new_position = this.transform.position;
        // 플레이어의 X좌표에 차이 값을 더해서 new_position의 X에 대입.
        new_position.x = this.player.transform.position.x + this.position_offset.x;

        // 아무 것도 아닌 상태.
        if (skill_damaged == false && stop_move == false)
        {
            new_position.y = targetPosY;
            // 보스 위치를 새로운 위치(new_position)로 갱신.
            transform.position = new Vector3(new_position.x,
                Mathf.Lerp(transform.position.y, new_position.y, Time.deltaTime),
                transform.position.z);
        }
        // 스킬에 맞아서 높이 올라갈 때.
        else if (skill_damaged == true && stop_move == false)
        {
            this.transform.position = new_position;
        }
        // 발판 위에 떨어져있을 때.
        else if(skill_damaged == true && stop_move == true)
        {
            transform.position = map_creator.boss_pos + Vector3.back;
        }
        // 기본 공격에 맞고 날아갈 때.
        else
        {
            new_position.x = this.transform.position.x;
            this.transform.position = new_position;
        }
    }

    public void PosYTracking()
    {
        if (skill_damaged == false)
        {
            targetPosY = player.transform.position.y;
            Debug.Log("tracking");
        }
    }

    public void CreateFireball()
    {
        if (skill_damaged == false && stop_move == false)
        {
            Instantiate(fireball, transform.position, Quaternion.identity);
            Debug.Log("Fire");
        }
    }

    public void SkillDamage()
    {
        skill_damaged = true;
        rigid.AddExplosionForce(1000f, transform.position - Vector3.up * 2f, 20f, 5f);
    }

    public void Recovery()
    {
        rigid.isKinematic = false;
        skill_damaged = false;
        map_creator.boss_pos = Vector3.zero;
        rigid.AddExplosionForce(800f, transform.position - Vector3.right, 20f, 0f);
        StartCoroutine(ResetPosition());
    }

    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(2f);

        stop_move = false;
        //skill_damaged = false;
    }
}
