using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopperControl : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator를 보관하는 변수.
    public GameRoot Game_Root = null;

    private Canvas Shop_Canvas = null;
    private Text Shop_Text;



    // Start is called before the first frame update
    void Start()
    {
        Shop_Canvas = this.transform.GetChild(0).gameObject.GetComponent<Canvas>();

        Shop_Text = Shop_Canvas.transform.GetChild(0).gameObject.GetComponent<Text>();
        Game_Root = GameObject.Find("GameRoot").GetComponent<GameRoot>();
        // MapCreator를 가져와서 멤버 변수 map_creator에 보관.
        map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라에게 나 안보이냐고 물어보고 안 보인다고 대답하면,
        if (this.map_creator.isDelete(this.gameObject))
        {
            GameObject.Destroy(this.gameObject); // 자기 자신을 삭제.
        }

        Shop_Text.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0.0f, 1.5f, 0.0f));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Game_Root.Shop_Open();
            
        }
    }
}
