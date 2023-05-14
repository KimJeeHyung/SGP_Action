using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    // Start is called before the first frame update

    public float HP { get; set; } // �÷��̾� ü��
    public float MAX_HP { get; set; } // �÷��̾� �ִ� ü��

    public int Revive_Num; // ��Ȱ Ƚ��

    [SerializeField]
    private Slider PlayerHP;

    [SerializeField]
    private PlayerControl Player_Control; // �÷��̾ ��Ʈ�� �ϴ� PlayerControll ��ũ��Ʈ

    void Start()
    {
        Revive_Num = 0;
        MAX_HP = 100;
        HP = MAX_HP;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHP.value = HP / MAX_HP;
    }

    public void Revive()
    {
        if(Revive_Num != 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, 3.0f, this.transform.position.z);
            Revive_Num -= 1;
            Player_Control.ResetJumpCount();
            Debug.Log("��Ȱ �������� �ڵ������� ���Ǿ����� ���� ��Ȱ Ƚ����" + Revive_Num);
            
        }

    }


}
