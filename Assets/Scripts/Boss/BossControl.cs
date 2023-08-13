using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    private BossMapCreator map_creator = null;

    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;
    private float targetPosY = 0f;

    private Rigidbody rigid;

    public GameObject spawner = null;
    public GameObject fireball = null;
    public GameObject fireball2 = null;
    public bool skill_damaged = false;
    public bool stop_move = false;
    public bool reset_pos = false;
    public float spread_power = 5f;
    public float interval = 0.1f;

    public float MaxHP { get; set; } = 100f;
    public float HP { get; set; }

    private int collision_count = 0;
    private bool is_thinking = false;

    // Start is called before the first frame update
    void Start()
    {
        map_creator = FindObjectOfType<BossMapCreator>();

        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        this.position_offset = this.transform.position - this.player.transform.position;

        rigid = GetComponent<Rigidbody>();

        HP = MaxHP;

        InvokeRepeating("PosYTracking", 5f, 5f);

        StartCoroutine(Think());
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
        else if (skill_damaged == true && stop_move == true)
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
            //Debug.Log("tracking");
        }
    }

    public enum State
    {
        FireballNormal,
        FireballSpread,
        FireballConsecutive,
        Think
    }

    IEnumerator Think()
    {
        State _State;

        is_thinking = true;

        yield return new WaitForSeconds(3f);

        _State = (State)Random.Range(0,3);

        switch (_State)
        {
            // 기본 발사.
            case State.FireballNormal:
                StartCoroutine(FireballNormal());
                break;
            // 확산.
            case State.FireballSpread:
                StartCoroutine(FireballSpread());
                break;
            // 연속 공격
            case State.FireballConsecutive:       
                StartCoroutine(FireballConsecutive());
                break;
            //다시 생각
            case State.Think:
                StartCoroutine(Think());
                break;


        }
    }

    IEnumerator FireballNormal()
    {
        Instantiate(fireball, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        StartCoroutine(Think());
    }

    IEnumerator FireballSpread()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject fb = Instantiate(fireball, transform.position, Quaternion.identity);

            Rigidbody fRigid = fb.GetComponent<Rigidbody>();
            Vector2 dirVec = new Vector2(-Mathf.Sin(Mathf.PI * i / 5),
                -Mathf.Cos(Mathf.PI * i / 5));
            
            fRigid.AddForce(dirVec.normalized * spread_power, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(Think());
    }

    IEnumerator FireballConsecutive()
    {
        Instantiate(fireball, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(interval);

        Instantiate(fireball, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(interval);

        Instantiate(fireball, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        StartCoroutine(Think());
    }


    public void SkillDamage()
    {
        skill_damaged = true;
        StopAllCoroutines();
        is_thinking = false;
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
        if (is_thinking == false)
        {
            StartCoroutine(Think());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collision_count++;
            Time.timeScale = 1f;
            if (collision_count == 2)
            {
                PlayerStat.Instance.SetHP(-50f);
                collision_count = 0;
            }

            Recovery();
        }
    }
}
