using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickController : MonoBehaviour
{
    public ProductController Product_Controller; // ������ ��ǰ���� �����ϴ� ProductController ���� ȭ�鿡 ����ϰų� �� ����.
                                                 // �ַ� ��ų ���.
    public ProductData Product_Data; // ������ ���� ������ ScriptableObject
    
    public PlayerStat Player_Stat; // �÷��̾� ����

    [SerializeField]
    private GameObject Skill_Prefab; // ��ų ������

    [SerializeField]
    private GameObject Skill_Slot; // ��ų ���� �θ� �ڽ� ��ü


    [SerializeField]
    private RecordCollector Record_Collector;

    private Text Price_Text;

    public int Buy_Offset; // ���� ���� �� �þ�� ��ȭ ��

    public int Buy_Num; // ���� ���� Ƚ��

    private int Price;


    // Start is called before the first frame update
    void Start()
    {
        Player_Stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        Product_Controller = GameObject.Find("ProductController").GetComponent<ProductController>();
        Record_Collector = GameObject.Find("RecordCollector").GetComponent<RecordCollector>();
        Price_Text = transform.Find("Price_Text").GetComponent<Text>();
        Price = int.Parse(Price_Text.text);
        Skill_Slot = GameObject.FindGameObjectWithTag("SkillSlot");
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Record_Collector.Coin_Num < Price)
            Price_Text.color = new Color(255, 0, 0);

        if(Buy_Num <= 0)
        {
            Price_Text.text = "�ִ� ���źҰ�";
            Price_Text.color = new Color(255, 0, 0);
        }
        if(Record_Collector.Coin_Num >= Price && Buy_Num != 0)
        {
            Price_Text.color = new Color(0, 255, 0);
        }
    }

    public void ButtonClick()
    {
        if(Record_Collector.Coin_Num >= Price && Buy_Num != 0)
        {
            Record_Collector.Coin_Num -= Price;
            Price += Buy_Offset;
            Price_Text.text = Price.ToString();
            Buy_Num -= 1;

            switch (Product_Data.Product_Type)
            {
                case ProductData.ProductType.Passive:
                    if(Product_Data.Product_Title == "HP ����")
                    {
                        Player_Stat.MAX_HP += Product_Data.increase;
                        Player_Stat.HP += Product_Data.increase;
                        Debug.Log("���� �ִ� ü����" + Player_Stat.MAX_HP +" �Դϴ� ");
                    }
                    break;
                case ProductData.ProductType.Use:
                    if(Product_Data.Product_Title == "��Ȱ")
                    {
                        Player_Stat.Revive_Num += 1;
                    }
                    break;
                case ProductData.ProductType.Skill:
                    Product_Controller.AddSkillDictionary(Product_Data);
                    GameObject Skill = Instantiate(Skill_Prefab);
                    Skill.GetComponent<Image>().sprite = Product_Data.Product_Image;
                    Skill.transform.Find("Skill_Key").GetComponent<Text>().text = Product_Data.Key_Code;
                    Skill.transform.SetParent(Skill_Slot.transform);
                    Product_Controller.Get_SkillObject(Product_Data.Product_Title,Skill);
                    Debug.Log("��ų ��ųʸ��� ������ �ƽ��ϴ� !");
                    break;
                default:
                    break;
            }
        }

        

    }
}
