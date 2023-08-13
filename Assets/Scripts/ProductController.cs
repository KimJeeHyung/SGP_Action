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

    [SerializeField]
    private GameObject Lightening;

    [SerializeField]
    private GameObject Slash;

    public GameObject newObjects;

    ProductData Product_Data;

    public LayerMask layerMask;

    public Collider[] hits;

    [SerializeField]
    private GameObject Skill_Slot; // 스킬 슬롯 부모 자식 객체
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonClickController.Revive != null)
        ButtonClickController.Revive.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + PlayerStat.Instance.GetReviveNum();
        if (PlayerStat.Instance.GetReviveNum() <= 0 && ButtonClickController.Revive != null)
        {
            Destroy(ButtonClickController.Revive.gameObject);
            ButtonClickController.Revive = null;
        }

        foreach (ProductData data in Skill) {

            if (data.Product_Title == "순보")
            { 
                GameObject skill;
                Skill_Product.TryGetValue(data.Product_Title, out skill);

                if (Input.GetKeyDown(KeyCode.Z) && skill.GetComponent<Image>().fillAmount >= 1.0f)
                {                 
                    Debug.Log("Z키 누름");
                    Collider[] hits = Physics.OverlapSphere(Player.position, 10.0f);
                    
                    if(hits.Length > 0)
                    {
                        foreach(Collider hit in hits)
                        {
                            if (hit.gameObject.CompareTag("Monster"))
                            {
                                Instantiate(Lightening, Player.transform.position, Quaternion.identity);
                                SoundManager.instance.PlaySE("Dash");
                                Debug.Log(hit.gameObject.tag + "가 주변에 있습니다 !");
                                Player.position = hit.transform.position + (Vector3.up * 0.05f) + (Vector3.left * 0.1f);
                                Instantiate(Lightening, Player.transform.position, Quaternion.identity);  
                                Destroy(hit.gameObject);
                                skill.GetComponent<Image>().fillAmount = 0.0f;
                                data.Current_Cool_Time = 0.0f;
                                break;
                            }
                        }
                       
                    }
                    
                }

                if(skill.GetComponent<Image>().fillAmount < 1.0f)
                {
                    skill.GetComponent<Image>().fillAmount = data.Current_Cool_Time / data.Cool_Time;
                    data.Current_Cool_Time = Mathf.Clamp(data.Current_Cool_Time, 0.0f, data.Cool_Time);
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
