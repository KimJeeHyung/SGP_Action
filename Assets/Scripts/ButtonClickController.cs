using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickController : MonoBehaviour
{
    public ProductController Product_Controller; // 구매한 상품들을 관리하는 ProductController 이후 화면에 출력하거나 할 것임.
                                                 // 주로 스킬 출력.
    public ProductData Product_Data; // 본인이 현재 구매한 ScriptableObject

    [SerializeField]
    private GameObject Skill_Prefab; // 스킬 프리팹

    [SerializeField]
    private GameObject Revive_Prefab; // 부활 프리팹

    [SerializeField]
    private GameObject Skill_Slot; // 스킬 슬롯 부모 자식 객체


    [SerializeField]
    private RecordCollector Record_Collector;

    private Text Price_Text;

    public int Buy_Offset; // 구매 했을 시 늘어나는 재화 수

    public int Buy_Num; // 구매 가능 횟수

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
            Price_Text.text = "최대 구매불가";
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
                    if(Product_Data.Product_Title == "HP 증가")
                    {
                        PlayerStat.Instance.SetMaxHP(Product_Data.increase);
                        PlayerStat.Instance.SetHP(Product_Data.increase);
                        Debug.Log("현재 최대 체력은 " + PlayerStat.Instance.GetMaxHP() +" 입니다 ");
                    }
                    break;
                case ProductData.ProductType.Use:
                    if(Product_Data.Product_Title == "부활")
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
                    Debug.Log("스킬 딕셔너리에 저장이 됐습니다 !");
                    break;
                default:
                    break;
            }
        }
    }
}
