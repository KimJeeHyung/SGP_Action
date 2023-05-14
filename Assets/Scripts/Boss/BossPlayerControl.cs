using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerControl : MonoBehaviour
{
    // ������ �ʿ��� �������� ���� ����.
    public static float ACCELERATION = 10.0f;           // ���ӵ�.
    public static float SPEED_MIN = 4.0f;               // �ӵ��� �ּڰ�.
    public static float SPEED_MAX = 8.0f;               // �ӵ��� �ִ�.
    public static float JUMP_HEIGHT_MAX = 3.0f;         // ���� ����.
    public static float JUMP_KEY_RELEASE_REDUCE = 0.5f; // ���� ���� ���ӵ�.

    // Player�� ���� ���¸� ��Ÿ���� �ڷ��� (*����ü)
    public enum STEP
    {
        NONE = -1,  // �������� ����.
        RUN = 0,    // �޸���.
        JUMP,       // ����.
        MISS,       // ����.
        NUM,        // ���°� �� ���� �ִ��� �����ش�(=3).
    };

    public STEP step = STEP.NONE;           // Player�� ���� ����.
    public STEP next_step = STEP.NONE;      // Player�� ���� ����.
    public float step_timer = 0.0f;         // ��� �ð�.
    private bool is_landed = false;         // �����ߴ°�.
    private bool is_key_released = false;   // ��ư�� �������°�.



    public static float NARAKU_HEIGHT = -5.0f;

    public float current_speed = 0.0f;          // ���� �ӵ�.
    public LevelControl level_control = null;   // LevelControl�� �����.
    public GameRoot game_root = null; // GameRoot�� �����.
    public BlockCreator block_creator = null;

    private float click_timer = 1.0f;       // ��ư�� ���� ���� �ð�
    private float CLICK_GRACE_TIME = 0.5f;  // �����ϰ� ���� �ǻ縦 �޾Ƶ��� �ð�

    public float current_time = 0.0f;
    public bool stage_cleared = false;
    public bool use_skill = false;
    private int current_level = 0;
    private float stage_time = 0.0f;

    public int current_power = 0;
    public int max_power = 50;

    [SerializeField]
    private int jump_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.next_step = STEP.RUN;
        block_creator = GameObject.Find("GameRoot").GetComponent<BlockCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(use_skill==true)
        {
            level_control.clear = true;
            use_skill = false;
        }

        move();

        if (current_power >= max_power)
        {
            current_power = max_power;
        }
    }

    private void move()
    {
        this.transform.Translate(new Vector3(current_speed * Time.deltaTime, 0.0f, 0.0f));

        Vector3 velocity = this.GetComponent<Rigidbody>().velocity; // �ӵ��� ����.
        this.current_speed = this.level_control.getPlayerSpeed();
        this.check_landed(); // ���� �������� üũ.

        if (this.is_landed && jump_count != 0)
        {
            jump_count = 0;
        }

        switch (this.step)
        {
            case STEP.RUN:
            case STEP.JUMP:
                // ���� ��ġ�� �Ѱ�ġ���� �Ʒ���.
                if (this.transform.position.y < NARAKU_HEIGHT)
                {
                    this.next_step = STEP.MISS; // '����' ���·� �Ѵ�.
                }
                break;
        }

        this.step_timer += Time.deltaTime; // ��� �ð��� �����Ѵ�.

        // ��ư�� ��������.
        if (Input.GetKeyDown(KeyCode.Space) && jump_count < 2)
        {
            jump_count += 1;
            this.click_timer = 0.0f; // Ÿ�̸Ӹ� ����.
        }
        // �׷��� ������.
        else
        {
            if (this.click_timer >= 0.0f)
            {
                this.click_timer += Time.deltaTime; // ��� �ð��� ���Ѵ�.
            }
        }

        // ���� ���°� ������ ���� ������ ������ ��ȭ�� �����Ѵ�.
        if (this.next_step == STEP.NONE)
        {
            // Player�� ���� ���·� �б�.
            switch (this.step)
            {
                case STEP.RUN: // �޸��� ���� ��.
                    //if (!this.is_landed)
                    //{
                    //    // �޸��� ���̰� �������� ���� ��� �ƹ��͵� ���� �ʴ´�.
                    //}
                    //else
                    //{
                    //    // �޸��� ���̰� �����߰� ���� ��ư�� ���ȴٸ�.
                    //    if (Input.GetMouseButtonDown(0))
                    //    {
                    //        // ���� ���¸� ������ ����.
                    //        this.next_step = STEP.JUMP;
                    //    }
                    //}
                    // click_timer�� 0�̻�, CLICK_GRACE_TIME���϶��.
                    if (0.0f <= this.click_timer && this.click_timer <= CLICK_GRACE_TIME)
                    {
                        // �����ߴٸ�.
                        
                        
                            this.click_timer = -1.0f;   // ��ư�� �������� ������ ��Ÿ���� -1.0f��.
                            this.next_step = STEP.JUMP; // ���� ���·� �Ѵ�.
                        
                    }
                    break;
                case STEP.JUMP: // ���� ���� ��.
                  
                    
                        // ���� ���̰� �����ߴٸ� ���� ���¸� ���� ������ ����.
                        this.next_step = STEP.RUN;
                    
                    break;
            }
        }

        // '���� ����'�� '���� ���� ����'�� �ƴ� ����(���°� ���� ����).
        while (this.next_step != STEP.NONE)
        {
            this.step = this.next_step; // '���� ����'�� '���� ����'�� ����.
            this.next_step = STEP.NONE; // '���� ����'�� '���� ����'���� ����.

            // ���ŵ� '���� ����'��.
            switch (this.step)
            {
                case STEP.JUMP: // '����'�� ��.
                    // �ְ� ������ ����(JUMP_HEIGHT_MAX)���� ������ �� �ִ� �ӵ��� ���.
                    velocity.y = Mathf.Sqrt(1.25f * 9.8f * PlayerControl.JUMP_HEIGHT_MAX);
                    // '��ư�� ���������� ��Ÿ���� �÷���'�� Ŭ�����Ѵ�.
                    this.is_key_released = false;
                    break;
            }
            // ���°� �������Ƿ� ��� �ð��� ���η� ����.
            this.step_timer = 0.0f;
        }

        // ���º��� �� ������ ���� ó��.
        switch (this.step)
        {
            case STEP.RUN: // �޸��� ���� ��.
                // �ӵ��� ���δ�.
                //velocity.x += PlayerControl.ACCELERATION * Time.deltaTime;
                // �ӵ��� �ְ� �ӵ� ������ ������.
                //if (Mathf.Abs(velocity.x) > PlayerControl.SPEED_MAX)
                //{
                //    // �ְ� �ӵ� ���� ���Ϸ� �����Ѵ�.
                //    velocity.x *= PlayerControl.SPEED_MAX /
                //    Mathf.Abs(this.GetComponent<Rigidbody>().velocity.x);
                //}
                // ������� ���� �ӵ��� �����ؾ� �� �ӵ��� ������.
                if (Mathf.Abs(velocity.x) > this.current_speed)
                {
                    // ���� �ʰ� �����Ѵ�.
                    velocity.x *= this.current_speed / Mathf.Abs(velocity.x);
                }
                break;
            case STEP.JUMP: // ���� ���� ��.
                do
                {
                    // '��ư�� ������ ����'�� �ƴϸ�.
                    if (!Input.GetKeyUp(KeyCode.Space))
                    {
                        break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    }
                    // �̹� ���ӵ� ���¸�(�� ���̻� �������� �ʵ���).
                    if (this.is_key_released)
                    {
                        break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    }
                    // ���Ϲ��� �ӵ��� 0 ���ϸ�(�ϰ� ���̶��).
                    if (velocity.y <= 0.0f)
                    {
                        break; // �ƹ��͵� ���� �ʰ� ������ ����������.
                    }
                    // ��ư�� ������ �ְ� ��� ���̶�� ���� ����.
                    // ������ ����� ���⼭ ��.
                    //velocity.y *= JUMP_KEY_RELEASE_REDUCE;

                    this.is_key_released = true;
                } while (false);
                break;
            case STEP.MISS:
                // ���ӵ�(ACCELERATION)�� ���� Player�� �ӵ��� ������ �� ����.
                velocity.x -= PlayerControl.ACCELERATION * Time.deltaTime;
                // Player�� �ӵ��� ���̳ʽ���.
                if (velocity.x < 0.0f)
                {
                    velocity.x = 0.0f; // 0���� �Ѵ�.
                }
                break;
        }
        // Rigidbody�� �ӵ��� ������ ���� �ӵ��� ����.
        // (�� ���� ���¿� ������� �Ź� ����ȴ�).
        this.GetComponent<Rigidbody>().velocity = velocity;
    }

    private void check_landed() // �����ߴ��� ����
    {
        this.is_landed = false; // �ϴ� false�� ����.
        do
        {
            Vector3 s = this.transform.position; // Player�� ���� ��ġ.
            Vector3 e = s + Vector3.down * 0.499f; // s���� �Ʒ��� 1.0f�� �̵��� ��ġ.

            RaycastHit hit;
            // s���� e ���̿� �ƹ��͵� ���� ��. *out: method ������ ������ ���� ��ȯ�� ���.
            if (!Physics.Linecast(s, e, out hit))
            {
                break; // �ƹ��͵� ���� �ʰ� do~while ������ ��������(Ż�ⱸ��).
            }

            // s���� e ���̿� ���� ���� �� �Ʒ��� ó���� ����.
            // ����, ���� ���¶��.
            if (this.step == STEP.JUMP)
            {
                // ��� �ð��� 3.0f �̸��̶��.
                if (this.step_timer < Time.deltaTime * 3.0f)
                {
                    break; // �ƹ��͵� ���� �ʰ� do~while ������ ��������(Ż�ⱸ��).
                }
            }

            // s���� e ���̿� ���� �ְ� JUMP ���İ� �ƴ� ���� �Ʒ��� ����.
            this.is_landed = true;
        } while (false);
        // ������ Ż�ⱸ.
    }

    public bool isPlayEnd()
    {
        // ������ �������� ����
        bool ret = false;
        switch (this.step)
        {
            case STEP.MISS: // MISS ���¶��,
                ret = true; // '�׾����'(true)��� �˷���
                break;
        }
        return (ret);
    }

    public void ResetJumpCount()
    {
        jump_count = 0;
    }
}

