using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickController : MonoBehaviour
{
    public ProductController Product_Controller; // ������ ��ǰ���� �����ϴ� ProductController ���� ȭ�鿡 ����ϰų� �� ����.
                                                 // �ַ� ��ų ���.
    public ProductData Product_Data; // ������ ���� ������ ScriptableObject

    [SerializeField]
    private GameObject Skill_Prefab; // ��ų ������

    [SerializeField]
    private GameObject Revive_Prefab; // ��Ȱ ������

    [SerializeField]
    private GameObject Skill_Slot; // ��ų ���� �θ� �ڽ� ��ü


    [SerializeField]
    private RecordCollector Record_Collector;

    private Text Price_Text;

    public int Buy_Offset; // ���� ���� �� �þ�� ��ȭ ��

    public int Buy_Num; // ���� ���� Ƚ��

    private int Price;

    static public GameObject Revive = null;

    // Start is called before the first frame update
    void Start()
    {
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
                        PlayerStat.Instance.SetMaxHP(Product_Data.increase);
                        PlayerStat.Instance.SetHP(Product_Data.increase);
                        Debug.Log("���� �ִ� ü���� " + PlayerStat.Instance.GetMaxHP() +" �Դϴ� ");
                    }
                    break;
                case ProductData.ProductType.Use:
                    if(Product_Data.Product_Title == "��Ȱ")
                    {
                        PlayerStat.Instance.SetReviveNum(1);
                        if (Revive == null)
                        {
                            Revive = Instantiate(Revive_Prefab);
                        }
                        Revive.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + PlayerStat.Instance.GetReviveNum();
                        Revive.transform.SetParent(Skill_Slot.transform);
                    }
                    break;
                case ProductData.ProductType.Skill:
                    Product_Controller.AddSkillDictionary(Product_Data);
                    GameObject Skill = Instantiate(Skill_Prefab);
                    //Skill.GetComponent<Image>().sprite = Product_Data.Product_Image;
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
