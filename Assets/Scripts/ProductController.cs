using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductController : MonoBehaviour
{

    private List<ProductData> Skill = new List<ProductData>();

    private Dictionary<string, GameObject> Skill_Product = new Dictionary<string, GameObject>();

    [SerializeField]
    private Transform Player;

    public GameObject newObjects;

    ProductData Product_Data;

    [SerializeField]
    private GameObject Skill_Slot; // 스킬 슬롯 부모 자식 객체
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ProductData data in Skill) {

            if (data.Product_Title == "대쉬")
            { 
                GameObject Skill;
                Skill_Product.TryGetValue(data.Product_Title, out Skill);

                if (Input.GetKeyDown(KeyCode.Z) && Skill.GetComponent<Image>().fillAmount >= 1.0f)
                {                 
                    Debug.Log("Z키 누름");
                    Player.GetComponent<Rigidbody>().AddForce(8.0f, 0.0f, 0.0f, ForceMode.VelocityChange);
                    Skill.GetComponent<Image>().fillAmount = 0.0f;
                    data.Current_Cool_Time = 0.0f;
                }

                if(Skill.GetComponent<Image>().fillAmount < 1.0f)
                {
                    Skill.GetComponent<Image>().fillAmount = data.Current_Cool_Time / data.Cool_Time;
                    data.Current_Cool_Time = Mathf.Clamp(data.Current_Cool_Time, 0.0f, 3.0f);
                    data.Current_Cool_Time += Time.deltaTime * 2.0f;
                }
            }

            }                     
        }
        
    public void Get_SkillObject(string Skill_Title,GameObject Skill)
    {
        Skill_Product.Add(Skill_Title,Skill);
    }

    public void AddSkillDictionary(ProductData skill_buy) { 
    
        Skill.Add(skill_buy);
    }

}
